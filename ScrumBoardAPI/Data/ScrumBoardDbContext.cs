using Microsoft.EntityFrameworkCore;

namespace ScrumBoardAPI.Data;

public class ScrumBoardDbContext: DbContext
{
    public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options): base(options) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

    }

}
