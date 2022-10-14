using AutoMapper;
using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Core.Repository;

public class TaskRepository : GenericRepository<ATask, int>, ITaskRepository
{
    public TaskRepository(ScrumBoardDbContext dbContext, IMapper autoMapper) : base(dbContext, autoMapper)
    {
    }
}
