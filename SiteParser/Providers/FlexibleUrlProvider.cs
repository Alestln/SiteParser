using AngleSharp.Dom;

namespace SiteParser.Providers;

public class FlexibleUrlProvider(Uri baseUri) : IProvider
{
    private readonly HttpClient _httpClient = new();

    public async Task<IDocument> GetDocumentAsync(string url)
    {
        var htmlContent = await _httpClient.GetStringAsync(new Uri(baseUri, url));
        var parser = new AngleSharp.Html.Parser.HtmlParser();
        return parser.ParseDocument(htmlContent);
    }
}