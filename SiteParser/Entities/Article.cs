using System.Text;

namespace SiteParser.Entities;

public class Article
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public string Url { get; set; }

    public int? ArticleId { get; set; }
    
    public ICollection<Article>? InternalLinks { get; set; }
}