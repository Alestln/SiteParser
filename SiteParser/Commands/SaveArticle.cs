using SiteParser.Entities;

namespace SiteParser.Commands;

public class SaveArticle (ParserDbContext context)
{
    public async Task SaveAsync(Article article, CancellationToken cancellationToken)
    {
        context.Add(article);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}