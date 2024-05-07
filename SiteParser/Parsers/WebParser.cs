using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public class WebParser : IParser
{
    public async Task<IEnumerable<Article>> ParseArticlesAsync(IDocument document)
    {
        var articleHeaders = document
            .QuerySelectorAll(".section-bordered.link-list h3");
        
        var articles = articleHeaders.Select(header => 
            Article.Create(
                header.TextContent.Trim(), 
                header.QuerySelector("a")?.GetAttribute("href")
                    ?? throw new InvalidOperationException("Article link not found.")
                )
            );

        return articles;
        
        // Конкурентна колекція для зберігання результатів паралельних викликів
        /*var articles = new ConcurrentBag<Article>();

        Parallel.ForEach(articleHeaders, header =>
        {
            articles.Add(ParseArticleFromHeader(header));
        });

        return articles;*/
    }
    
    public async Task<IEnumerable<Article>> ParseInternalLinks(IDocument document)
    {
        return new List<Article>();
    }
}