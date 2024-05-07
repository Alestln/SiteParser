using SiteParser.Entities;

namespace SiteParser.Services;

public class SaveArticles (ParserDbContext context)
{
    public async Task SaveAsync(IEnumerable<Article> articles, CancellationToken cancellationToken)
    {
        await context.AddRangeAsync(articles, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}