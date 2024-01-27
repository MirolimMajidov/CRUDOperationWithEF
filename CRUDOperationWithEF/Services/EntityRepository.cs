using Microsoft.EntityFrameworkCore;
using MyUser.Models;

namespace MyUser.Services;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    readonly UserContext _context;
    readonly DbSet<TEntity> _dbSet;

    public EntityRepository(UserContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_dbSet.AsNoTracking());
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<TEntity> CreateAsync(TEntity item)
    {
        item.Id = Guid.NewGuid();
        await _dbSet.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<TEntity> UpdateAsync(TEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<bool> DeleteAsync(TEntity item)
    {
        try
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}