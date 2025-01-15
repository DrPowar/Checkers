using Checkers.Domain.Interfaces.Repositories;
using Checkers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Infrastructure.Repositories;

public abstract class BaseRepository<T>(CheckersDbContext context) : IBaseRepository<T>
    where T : class
{
    protected readonly CheckersDbContext Context = context ?? throw new ArgumentNullException(nameof(context));

    public virtual void Add(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        Context.AddAsync(entity, cancellationToken);
    }

    public virtual async Task<T?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> All(CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual void Update(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        Context.Update(entity);
    }

    public virtual void Delete(T entity, CancellationToken cancellationToken = default)
    {
        Context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken) > 0;
    }
}