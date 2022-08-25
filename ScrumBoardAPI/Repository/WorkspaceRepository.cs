using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Repository;

public class WorkspaceRepository : GenericRepository<Workspace, int>, IWorkspaceRepository
{
    public WorkspaceRepository(ScrumBoardDbContext dbContext) : base(dbContext)
    {
    }
}
