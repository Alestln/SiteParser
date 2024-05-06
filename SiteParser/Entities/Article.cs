namespace SiteParser.Entities;

public class Article
{
    public string Title { get; set; }

    public string Url { get; set; }

    public IEnumerable<Article> InternalLinks { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}\nUrl: {Url}\n\n";
    }
}