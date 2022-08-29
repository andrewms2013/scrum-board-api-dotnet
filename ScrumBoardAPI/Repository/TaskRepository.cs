using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Repository;

public class TaskRepository : GenericRepository<ATask, int>, ITaskRepository
{
    public TaskRepository(ScrumBoardDbContext dbContext) : base(dbContext)
    {
    }
}
