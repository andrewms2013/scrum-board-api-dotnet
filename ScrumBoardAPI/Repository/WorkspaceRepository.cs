using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Exceptions;

namespace ScrumBoardAPI.Repository;

public class WorkspaceRepository : GenericRepository<Workspace, int>, IWorkspaceRepository
{
    private readonly UserManager<AUser> _userManager;

    public WorkspaceRepository(ScrumBoardDbContext dbContext, UserManager<AUser> userManager, IMapper autoMapper) : base(dbContext, autoMapper)
    {
        _userManager = userManager;
    }


    public async Task<Workspace> CreateWorkspace(string name, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var workspace = new Workspace(name, userId);
        workspace.Users = new List<AUser> {user};

        return await AddAsync(workspace);
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

    public async Task AddUserToWorkspace(int workspaceId, string userName)
    {
        var workspace = await GetWorkspaceWithDetails(workspaceId);

        if (workspace == null)
        {
            throw new NotFoundException("Workspace was not found");
        }

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new NotFoundException("User was not found");
        }

        workspace.Users.Add(user);

        await UpdateAsync(workspace);
    }

    public async Task RemoveUserFromWorkspace(int workspaceId, string userName)
    {
        var workspace = await GetWorkspaceWithDetails(workspaceId);

        if (workspace == null)
        {
            throw new NotFoundException("Workspace was not found");
        }

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new NotFoundException("User was not found");
        }

        workspace.Users.Remove(user);

        await UpdateAsync(workspace);
    }
}
