using AngleSharp.Dom;

namespace SiteParser.Providers;

public class FlexibleUrlProvider(Uri baseUri) : IProvider
{
    private readonly HttpClient _httpClient = new();

    public async Task<IDocument> GetDocumentAsync(string url)
    {
        // Если URL абсолютный, используем его напрямую
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri absoluteUri))
        {
            return await GetDocumentFromAbsoluteUrl(absoluteUri);
        }

        // Пытаемся преобразовать относительный URL в абсолютный
        if (Uri.TryCreate(baseUri, url, out absoluteUri))
        {
            return await GetDocumentFromAbsoluteUrl(absoluteUri);
        }

        throw new ArgumentException("Invalid URL format.");
    }
    
    private async Task<IDocument> GetDocumentFromAbsoluteUrl(Uri absoluteUri)
    {
        var htmlContent = await _httpClient.GetStringAsync(absoluteUri);
        var parser = new AngleSharp.Html.Parser.HtmlParser();
        return parser.ParseDocument(htmlContent);
    }
}