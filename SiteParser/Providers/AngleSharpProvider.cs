using AngleSharp.Dom;

namespace SiteParser.Providers;

public class AngleSharpProvider(Uri baseUri) : IProvider
{
    private readonly HttpClient _httpClient = new();

    public async Task<IDocument> GetDocumentAsync(string url)
    {
        return new AngleSharp.Html.Parser.HtmlParser().ParseDocument(await _httpClient.GetStringAsync(new Uri(baseUri, url)));
    }
}