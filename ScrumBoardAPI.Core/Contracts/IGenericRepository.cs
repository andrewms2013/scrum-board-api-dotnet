using ScrumBoardAPI.Core.Models.Paging;

namespace ScrumBoardAPI.Core.Contracts;

public interface IGenericRepository<T, K>
    where T : class
{
    Task<ResultType?> GetAsync<ResultType>(K? id) where ResultType : class;

    Task<List<ResultType>> GetAllAsync<ResultType>();

    Task<PagedResult<ResultType>> GetAllAsync<ResultType>(QueryParameters parameters);

    Task<ResultType> AddAsync<SourceType, ResultType>(SourceType entity);

    Task DeleteAsync(K? id);

    Task UpdateAsync<SourceType>(K id, SourceType entity);

    Task<bool> Exists(K? id);
}
