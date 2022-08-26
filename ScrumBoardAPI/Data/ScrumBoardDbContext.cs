using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data.Configurations;
namespace ScrumBoardAPI.Data;

public class ScrumBoardDbContext: IdentityDbContext<AUser>
{
    public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options): base(options) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserCofiguration());
        modelBuilder.ApplyConfiguration(new WorkspaceCofiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>() { RoleId = "1", UserId = "1" }
        );
    }

    public DbSet<ATask>? ATask { get; set; }

    public DbSet<Workspace>? Workspace { get; set; }

}
