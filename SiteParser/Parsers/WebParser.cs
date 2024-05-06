using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public class WebParser(IDocument document) : IParser
{
    public async Task<IEnumerable<Article>> ParseArticlesAsync()
    {
        var articleHeaders = document.QuerySelectorAll(".section-bordered.link-list h3");
        var articles = articleHeaders.Select(ParseArticleFromHeader);

        return articles;
    }

    private Article ParseArticleFromHeader(IElement header)
    {
        return new Article()
        {
            Title = header.TextContent.Trim(),
            Url = header.QuerySelector("a")?.GetAttribute("href")
                  ?? throw new InvalidOperationException("Article link not found.")
        };
    }

    public async Task<IEnumerable<Article>> ParseInternalLinks(IDocument document)
    {
        return new List<Article>();
    }
}