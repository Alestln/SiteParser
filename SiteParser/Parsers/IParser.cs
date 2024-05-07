using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public interface IParser
{
    ValueTask<List<Article>> ParseArticles(IDocument document);

    ValueTask<List<Article>> ParseInternalLinks(IDocument document);
}