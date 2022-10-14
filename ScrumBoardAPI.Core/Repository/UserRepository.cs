using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Core.Repository;

public class UserRepository : GenericRepository<AUser, string>, IUserRepository
{
    public UserRepository(ScrumBoardDbContext dbContext, IMapper autoMapper) : base(dbContext, autoMapper)
    {
    }

    public async Task<bool> CanUserAccessWorkspace(string userId, int workspaceId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .Include(u => u.Workspaces)
            .SingleOrDefaultAsync();

        if (user is null) {
            return false;
        }

        return user.Workspaces.Any(workspace => workspaceId == workspace.Id);
    }

    public async Task<bool> IsUserWorkspaceAdmin(string userId, int workspaceId)
    {
        var workspace = await _dbContext.Workspace
            .Where(x => x.Id == workspaceId)
            .SingleOrDefaultAsync();

        if (workspace is null) {
            return false;
        }

        return workspace.AdminId == userId;
    }
}
