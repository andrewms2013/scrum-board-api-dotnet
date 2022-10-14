using ScrumBoardAPI.Core.Models.Paging;

namespace ScrumBoardAPI.Core.Contracts;

public interface IGenericRepository<T, K>
    where T : class
{
    Task<T?> GetAsync(K? id);

    Task<List<T>> GetAllAsync();

    Task<PagedResult<ResultType>> GetAllAsync<ResultType>(QueryParameters parameters);

    Task<T> AddAsync(T entity);

    Task DeleteAsync(K? id);

    Task UpdateAsync(T entity);

    Task<bool> Exists(K? id);
}
