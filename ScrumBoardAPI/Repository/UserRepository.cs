using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Repository;

public class UserRepository : GenericRepository<AUser, string>, IUserRepostory
{
    public UserRepository(ScrumBoardDbContext dbContext) : base(dbContext)
    {
    }
}
