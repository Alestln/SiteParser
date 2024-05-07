using System.Collections;
using System.Diagnostics;
using SiteParser.Entities;
using SiteParser.Parsers;
using SiteParser.Providers;

namespace SiteParser;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://refactoring.guru/refactoring/smells";

        // TODO: Get BaseUri
        var provider = new FlexibleUrlProvider(new Uri("https://refactoring.guru"));
        var webParser = new WebParser();

        Stopwatch sw = new Stopwatch();
        
        sw.Start();
        
        var articles = 
            await webParser.ParseArticlesAsync(await provider.GetDocumentAsync(url));
        
        sw.Stop();

        // не работает
        articles.First().InternalLinks = new List<Article>() { Article.Create("sdfsf", "sdfsdf8s9d7f") };
        Console.WriteLine($"Parse articles time: {sw.ElapsedMilliseconds}");
        
        sw.Restart();
        
        foreach (var article in articles)
        {
            article.InternalLinks = await webParser.ParseInternalLinks(await provider.GetDocumentAsync(article.Url));
        }
        
        sw.Stop();
        Console.WriteLine($"Parse internal links time: {sw.ElapsedMilliseconds}");
    }
}
