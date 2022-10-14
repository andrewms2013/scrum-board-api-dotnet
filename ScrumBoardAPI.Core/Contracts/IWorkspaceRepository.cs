using ScrumBoardAPI.Core.Contracts;
using ScrumBoardAPI.Core.Models.Paging;
using ScrumBoardAPI.Data;

public interface IWorkspaceRepository : IGenericRepository<Workspace, int> {
    Task<List<ResultType>> GetWorkspacesByUserId<ResultType>(string id);

    Task<PagedResult<ResultType>> GetPagedWorkspacesByUserId<ResultType>(string id, QueryParameters parameters);

    Task<ResultType?> GetWorkspaceWithDetails<ResultType>(int workspaceId) where ResultType : class;

    Task<ResultType> CreateWorkspace<ResultType>(string name, string userId);

    Task RenameWorkspace(int workspaceId, string name);

    Task AddUserToWorkspace(int workspaceId, string userName);

    Task RemoveUserFromWorkspace(int workspaceId, string userName);
}
