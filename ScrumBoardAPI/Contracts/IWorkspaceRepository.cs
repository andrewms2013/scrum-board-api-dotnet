
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;

public interface IWorkspaceRepository : IGenericRepository<Workspace, int> {
    Task<IList<Workspace>?> GetWorkspacesByUserId(string id);

    Task<Workspace?> GetWorkspaceWithDetails(int workspaceId);

    Task<bool> CanUserAccessWorkspace(string userId, int workspaceId);

    Task<bool> IsUserWorkspaceAdmin(string userId, int workspaceId);
}
