using SiteParser.Parsers;
using SiteParser.Providers;

namespace SiteParser;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://refactoring.guru/refactoring/smells";

        var provider = new WebProvider();
        var webParser = new WebParser(await provider.GetDocumentAsync(url));

        var articles = await webParser.ParseArticlesAsync();

        foreach (var article in articles)
        {
            Console.WriteLine(article);
        }
    }
}
