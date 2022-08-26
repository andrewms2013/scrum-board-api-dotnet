using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class WorkspaceCofiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasData(
            new Workspace
            {
                Id = 1,
                Name = "Workspace 1",
                AdminId = "1"
            },
            new Workspace
            {
                Id = 2,
                Name = "Workspace 2",
                AdminId = "1"
            }
        );

        builder.HasMany(p => p.Users)
            .WithMany(p => p.Workspaces)
            .UsingEntity(j => j.HasData(
                new { WorkspacesId = 1, UsersId = "1" },
                new { WorkspacesId = 2, UsersId = "1" }
            ));
    }
}
