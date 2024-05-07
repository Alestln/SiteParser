using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public class WebParser(Uri baseUri) : IParser
{
    public ValueTask<List<Article>> ParseArticles(IDocument document)
    {
        return ValueTask.FromResult(document
            .QuerySelectorAll(".section-bordered.link-list h3")
            .Select(header => new Article()
            {
                Title = header.TextContent,
                Url = new Uri(baseUri, header.QuerySelector("a")?.GetAttribute("href")
                                       ?? throw new InvalidOperationException("Article link not found.")).AbsoluteUri
            }).ToList());

        // Конкурентна колекція для зберігання результатів паралельних викликів
        /*var articles = new ConcurrentBag<Article>();

        Parallel.ForEach(articleHeaders, header =>
        {
            articles.Add(ParseArticleFromHeader(header));
        });

        return articles;*/
    }
    
    public ValueTask<List<Article>> ParseInternalLinks(IDocument document)
    {
        return ValueTask.FromResult(document
            .QuerySelectorAll(".relations.link-list a[href]")
            .Select(element => new Article
            {
                Title = element.TextContent,
                Url = new Uri(baseUri, element.GetAttribute("href")
                              ?? throw new InvalidOperationException("Article link not found.")).AbsoluteUri
            }).ToList());
    }
}