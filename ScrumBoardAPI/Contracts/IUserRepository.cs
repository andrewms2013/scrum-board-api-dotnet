
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;

public interface IUserRepository : IGenericRepository<AUser, string> {
    Task<bool> CanUserAccessWorkspace(string userId, int workspaceId);

    Task<bool> IsUserWorkspaceAdmin(string userId, int workspaceId);
}
