using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductService.Api.Domain.Entities;

namespace ProductService.Api.Data.Interceptors;

public class CreatedAtInterceptor: SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateCreatedAtProperties(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateCreatedAtProperties(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private void UpdateCreatedAtProperties(DbContext context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<Entity>();
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }
    }
}