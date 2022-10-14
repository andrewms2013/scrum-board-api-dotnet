using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Core.Contracts;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Core.Models.Paging;
using ScrumBoardAPI.Core.Exceptions;

namespace ScrumBoardAPI.Core.Repository;

public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
{
    protected readonly ScrumBoardDbContext _dbContext;
    protected readonly IMapper _autoMapper;

    public GenericRepository(ScrumBoardDbContext dbContext, IMapper autoMapper)
    {
        _dbContext = dbContext;
        _autoMapper = autoMapper;
    }

    public async Task<ResultType> AddAsync<SourceType, ResultType>(SourceType entity)
    {
        var sourceEntity = _autoMapper.Map<T>(entity);

        await _dbContext.AddAsync(sourceEntity);
        await _dbContext.SaveChangesAsync();

        return _autoMapper.Map<ResultType>(sourceEntity);
    }

    public async Task DeleteAsync(K? id)
    {
        var entity = await GetAsync<T>(id);
        if (entity is null) {
            return;
        }

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(K? id)
    {
        var entity = await GetAsync<T>(id);
        return entity != null;
    }

    public async Task<PagedResult<ResultType>> GetAllAsync<ResultType>(QueryParameters parameters)
    {
        var totalSize = _dbContext.Set<T>().Count();
        var query = _dbContext.Set<T>()
            .Skip(parameters.PageNumber * parameters.PageSize)
            .Take(parameters.PageSize);
        var items = await _autoMapper.ProjectTo<ResultType>(query).ToListAsync();

        return new PagedResult<ResultType> {
            Items = items,
            TotalCount = totalSize
        };
    }

    public async Task<List<ResultType>> GetAllAsync<ResultType>()
    {
        var query = _dbContext.Set<T>();
        return await _autoMapper.ProjectTo<ResultType>(query).ToListAsync();
    }

    public async Task<ResultType?> GetAsync<ResultType> (K? id) where ResultType : class
    {
        if (id is null) {
            return null;
        }

        var item = await _dbContext.Set<T>().FindAsync(id);

        if (item is null) {
            return null;
        }

        return _autoMapper.Map<ResultType>(item);
    }


    public async Task UpdateAsync<SourceType>(K id, SourceType entity)
    {
        var dbEntity = await GetAsync<T>(id);
        if (dbEntity is null) {
            throw new NotFoundException($"Entity with id ${id} was not found");
        }
        _autoMapper.Map(entity, dbEntity);
        _dbContext.Update(dbEntity);
        await _dbContext.SaveChangesAsync();
    }
}
