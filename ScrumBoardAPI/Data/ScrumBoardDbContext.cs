using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;

namespace ScrumBoardAPI.Data;

public class ScrumBoardDbContext: DbContext
{
    public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options): base(options) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AUser>().HasMany(t => t.AssignedTasks)
            .WithOne(g => g.Assignee)
            .HasForeignKey(g => g.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AUser>().HasMany(t => t.CreatedTasks)
            .WithOne(g => g.Creator)
            .HasForeignKey(g => g.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AUser>()
            .HasMany(p => p.Workspaces)
            .WithMany(p => p.Users)
            .UsingEntity<WorkspaceUser>(
                j => j
                    .HasOne(pt => pt.Workspace)
                    .WithMany(t => t.WorkspaceUsers)
                    .HasForeignKey(pt => pt.WorkspaceId),
                j => j
                    .HasOne(pt => pt.ApplicationUser)
                    .WithMany(p => p.WorkspaceUsers)
                    .HasForeignKey(pt => pt.UserId),
                j =>
                {
                    j.HasKey(t => new { t.WorkspaceId, t.UserId });
                });


        modelBuilder.Entity<AUser>().HasData(
            new AUser
            {
                Id = "1",
                Name = "John Doe",
                Email = "example@example.com",
                PasswordHash = "pass"
            },
            new AUser
            {
                Id = "2",
                Name = "Jane Doe",
                Email = "example2@example.com",
                PasswordHash = "pass2"
            }
        );

        modelBuilder.Entity<Workspace>().HasData(
            new Workspace
            {
                Id = 1,
                Name = "Workspace 1"
            },
            new Workspace
            {
                Id = 2,
                Name = "Workspace 2"
            }
        );

        modelBuilder.Entity<WorkspaceUser>().HasData(
            new WorkspaceUser
            {
                WorkspaceId = 1,
                UserId = "1"
            },
            new WorkspaceUser
            {
                WorkspaceId = 1,
                UserId = "2"
            }
        );

        modelBuilder.Entity<Task>().HasData(
            new Task
            {
                Id = 1,
                Name = "Task 1",
                Description = "Task 1 description",
                AssigneeId = "1",
                CreatorId = "1",
                WorkspaceId = 1,
                Priority = "High"
            },
            new Task
            {
                Id = 2,
                Name = "Task 2",
                Description = "Task 2 description",
                AssigneeId = "1",
                CreatorId = "2",
                WorkspaceId = 1,
                Priority = "Medium"
            }
        );
    }

    public DbSet<ScrumBoardAPI.Data.AUser>? AUser { get; set; }

}
