using AngleSharp;
using AngleSharp.Dom;

namespace SiteParser.Providers;

public class WebProvider : IWebProvider
{
    private readonly HttpClient _httpClient;

    public WebProvider()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IDocument> GetDocumentAsync(string url)
    {
        var htmlContent = await _httpClient.GetStringAsync(url);
        var parser = new AngleSharp.Html.Parser.HtmlParser();
        return parser.ParseDocument(htmlContent);
    }
}