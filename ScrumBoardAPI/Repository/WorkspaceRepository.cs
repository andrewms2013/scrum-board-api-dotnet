using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Repository;

public class WorkspaceRepository : GenericRepository<Workspace, int>, IWorkspaceRepository
{
    public WorkspaceRepository(ScrumBoardDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<IList<Workspace>?> GetWorkspacesByUserId(string id)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == id)
            .Include(u => u.Workspaces)
            .SingleOrDefaultAsync();

        return user?.Workspaces ?? new List<Workspace>();
    }

    public async Task<Workspace?> GetWorkspaceWithDetails(int id)
    {
        var workspace = await _dbContext.Workspace
            .Where(x => x.Id == id)
            .Include(w => w.Tasks)
            .Include(w => w.Admin)
            .Include(w => w.Users)
            .SingleOrDefaultAsync();

        return workspace;
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
