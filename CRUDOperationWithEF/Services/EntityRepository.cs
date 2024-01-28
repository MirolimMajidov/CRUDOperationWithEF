using Microsoft.EntityFrameworkCore;
using MyUser.Models;
using System.Linq;

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

    public async Task<IEnumerable<TEntity>> GetAllAsync(int? from, int? size)
    {
        var items = _dbSet.AsNoTracking();
        if (from is not null)
            items = items.Skip((int)from);

        if (size is not null)
            items = items.Take((int)size);

        return await Task.FromResult(items);
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

    public async Task<TEntity> UpdateAsync(Guid id, TEntity item)
    {
        var _item = await GetByIdAsync(id);
        if (_item is not null)
        {
            _context.Entry(_item).State = EntityState.Detached;
            item.Id = id;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        return null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);
        if (item is not null)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}