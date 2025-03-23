using UserControl.Model.Entities;

namespace UserControl.Core.Abstractions.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
{
    IQueryable<TEntity> Query();
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Delete(Guid id, CancellationToken cancellationToken);
}
