using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class UserCofiguration : IEntityTypeConfiguration<AUser>
{
    public void Configure(EntityTypeBuilder<AUser> builder)
    {
        builder.HasMany(t => t.AssignedTasks!)
            .WithOne(g => g.Assignee!)
            .HasForeignKey(g => g.AssigneeId!)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.CreatedTasks!)
            .WithOne(g => g.Creator!)
            .HasForeignKey(g => g.CreatorId!)
            .OnDelete(DeleteBehavior.Cascade);

        // builder.HasMany(p => p.Workspaces)
        //     .WithMany(p => p.Users)
        //     .UsingEntity(j => j.HasData(new { WorkspacesId = 1, UsersId = "1" }));
    }
}
