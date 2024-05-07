using AngleSharp.Dom;

namespace SiteParser.Providers;

public interface IProvider
{
    Task<IDocument> GetDocumentAsync(string url);
}