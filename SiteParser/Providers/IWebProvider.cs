using AngleSharp.Dom;

namespace SiteParser.Providers;

public interface IWebProvider
{
    Task<IDocument> GetDocumentAsync(string url);
}