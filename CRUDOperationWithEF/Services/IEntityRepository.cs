using MyUser.Models;

namespace MyUser.Services;

public interface IEntityRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity item);
    Task<TEntity> UpdateAsync(TEntity item);
    Task<bool> DeleteAsync(TEntity item);
}