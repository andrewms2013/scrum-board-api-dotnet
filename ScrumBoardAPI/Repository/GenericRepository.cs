using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Repository;

public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
{
    protected readonly ScrumBoardDbContext _dbContext;

    public GenericRepository(ScrumBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(K? id)
    {
        var entity = await GetAsync(id);
        if (entity is null) {
            return;
        }

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(K? id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(K? id)
    {
        if (id is null) {
            return null;
        }

        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
