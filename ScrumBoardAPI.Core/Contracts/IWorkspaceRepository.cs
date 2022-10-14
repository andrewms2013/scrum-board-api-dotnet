using ScrumBoardAPI.Core.Contracts;
using ScrumBoardAPI.Data;

public interface IWorkspaceRepository : IGenericRepository<Workspace, int> {
    Task<IList<Workspace>?> GetWorkspacesByUserId(string id);

    Task<Workspace?> GetWorkspaceWithDetails(int workspaceId);

    Task<Workspace> CreateWorkspace(string name, string userId);

    Task AddUserToWorkspace(int workspaceId, string userName);

    Task RemoveUserFromWorkspace(int workspaceId, string userName);
}
