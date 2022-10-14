using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class WorkspaceCofiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasData(
            new Workspace("Workspace 1", "1")
            {
                Id = 1
            },
            new Workspace("Workspace 2", "1")
            {
                Id = 2
            }
        );

        builder.HasMany(p => p.Users)
            .WithMany(p => p.Workspaces)
            .UsingEntity(j => j.HasData(
                new { WorkspacesId = 1, UsersId = "1" },
                new { WorkspacesId = 2, UsersId = "1" }
            ));

        builder.HasMany(t => t.Tasks)
            .WithOne(g => g.Workspace)
            .HasForeignKey(g => g.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
