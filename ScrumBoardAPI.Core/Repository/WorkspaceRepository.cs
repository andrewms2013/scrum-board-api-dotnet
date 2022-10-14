using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Core.Exceptions;
using ScrumBoardAPI.Core.Models.Paging;

namespace ScrumBoardAPI.Core.Repository;

public class WorkspaceRepository : GenericRepository<Workspace, int>, IWorkspaceRepository
{
    private readonly UserManager<AUser> _userManager;

    public WorkspaceRepository(ScrumBoardDbContext dbContext, UserManager<AUser> userManager, IMapper autoMapper) : base(dbContext, autoMapper)
    {
        _userManager = userManager;
    }

    public async Task<ResultType> CreateWorkspace<ResultType>(string name, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var workspace = new Workspace(name, userId);
        workspace.Users = new List<AUser> {user};

        return await AddAsync<Workspace, ResultType>(workspace);
    }

    public async Task<List<ResultType>> GetWorkspacesByUserId<ResultType>(string id)
    {
        var query = _dbContext.Workspace
            .Where(x => x.Users.Any(user => user.Id == id));

        return await _autoMapper.ProjectTo<ResultType>(query).ToListAsync();
    }

    public async Task<ResultType?> GetWorkspaceWithDetails<ResultType>(int id) where ResultType : class
    {
        var workspace = await _dbContext.Workspace
            .Where(x => x.Id == id)
            .Include(w => w.Tasks)
            .Include(w => w.Admin)
            .Include(w => w.Users)
            .SingleOrDefaultAsync();

        if (workspace is null) {
            return null;
        }

        return _autoMapper.Map<ResultType>(workspace);
    }

    public async Task AddUserToWorkspace(int workspaceId, string userName)
    {
        var workspace = await GetWorkspaceWithDetails<Workspace>(workspaceId);

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

        await UpdateAsync(workspaceId, workspace);
    }

    public async Task RemoveUserFromWorkspace(int workspaceId, string userName)
    {
        var workspace = await GetWorkspaceWithDetails<Workspace>(workspaceId);

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

        await UpdateAsync(workspaceId, workspace);
    }

    public async Task<PagedResult<ResultType>> GetPagedWorkspacesByUserId<ResultType>(string id, QueryParameters parameters)
    {
        var totalSize = _dbContext.Workspace
            .Where(x => x.Users.Any(user => user.Id == id))
            .Count();

        var query = _dbContext.Workspace
            .Where(x => x.Users.Any(user => user.Id == id))
            .Skip(parameters.PageNumber * parameters.PageSize)
            .Take(parameters.PageSize);

        var items = await _autoMapper.ProjectTo<ResultType>(query).ToListAsync();

        return new PagedResult<ResultType> {
            Items = items,
            TotalCount = totalSize
        };
    }

    public async Task RenameWorkspace(int workspaceId, string newName)
    {
        var workspace = await GetAsync<Workspace>(workspaceId);

        if (workspace == null)
        {
            throw new NotFoundException($"Workspace with id ${workspaceId} was not found");
        }

        workspace.Name = newName;

        await UpdateAsync(workspaceId, workspace);
    }
}
