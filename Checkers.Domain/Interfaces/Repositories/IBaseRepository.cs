namespace Checkers.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    void Add(T entity, CancellationToken cancellationToken = default);
    Task<T?> Get(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> All(CancellationToken cancellationToken = default);
    void Update(T entity, CancellationToken cancellationToken = default);
    void Delete(T entity, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}