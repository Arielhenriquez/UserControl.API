using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserControl.Core.Abstractions.Repositories;
using UserControl.Core.Exceptions;
using UserControl.Model.Context;
using UserControl.Model.Entities;

namespace UserControl.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
  where TEntity : class, IBaseEntity
{
    private readonly UserContactDbContext _userContactDbContext;

    public BaseRepository(UserContactDbContext userContactDbContext)
    {
        _userContactDbContext = userContactDbContext;
    }

    public virtual IQueryable<TEntity> Query()
    {
        return _userContactDbContext.Set<TEntity>().AsQueryable();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Query().ToListAsync();
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Query().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);

        if (entity is null) throw new NotFoundException(typeof(TEntity).Name, id);

        return entity;
    }
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _userContactDbContext.AddAsync(entity, cancellationToken);
            await _userContactDbContext.SaveChangesAsync(cancellationToken);
            return result.Entity; 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var _entity = await GetById(entity.Id, cancellationToken);
        Type type = typeof(TEntity);
        PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var item in propertyInfo)
        {
            var fieldValue = item.GetValue(entity);
            if (fieldValue != null)
            {
                item.SetValue(_entity, fieldValue);
            }
        }
        await _userContactDbContext.SaveChangesAsync(cancellationToken);
        return _entity;
    }

    public async Task<TEntity> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetById(id, cancellationToken);

        var result = _userContactDbContext.Remove(entity);
        await _userContactDbContext.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
