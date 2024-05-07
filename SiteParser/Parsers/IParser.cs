using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public interface IParser
{
    Task<IEnumerable<Article>> ParseArticlesAsync(IDocument document);

    Task<List<Article>> ParseInternalLinks(IDocument document);
}