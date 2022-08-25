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


        // modelBuilder.Entity<Workspace>().HasData(
        //     new Workspace
        //     {
        //         Id = 1,
        //         Name = "Workspace 1"
        //     },
        //     new Workspace
        //     {
        //         Id = 2,
        //         Name = "Workspace 2"
        //     }
        // );

        // modelBuilder.Entity<ATask>().HasData(
        //     new ATask
        //     {
        //         Id = 1,
        //         Name = "Task 1",
        //         Description = "Task 1 description",
        //         AssigneeId = "1",
        //         CreatorId = "1",
        //         WorkspaceId = 1,
        //         Priority = "High"
        //     },
        //     new ATask
        //     {
        //         Id = 2,
        //         Name = "Task 2",
        //         Description = "Task 2 description",
        //         AssigneeId = "1",
        //         CreatorId = "2",
        //         WorkspaceId = 1,
        //         Priority = "Medium"
        //     }
        // );
    }

    public DbSet<ATask>? ATask { get; set; }

    public DbSet<Workspace>? Workspace { get; set; }

}
