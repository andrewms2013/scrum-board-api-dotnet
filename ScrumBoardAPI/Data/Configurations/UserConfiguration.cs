using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class UserCofiguration : IEntityTypeConfiguration<AUser>
{
    public void Configure(EntityTypeBuilder<AUser> builder)
    {
        builder.HasMany(t => t.AssignedTasks)
            .WithOne(g => g.Assignee)
            .HasForeignKey(g => g.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.CreatedTasks)
            .WithOne(g => g.Creator)
            .HasForeignKey(g => g.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.AdministratedWorkspaces)
            .WithOne(g => g.Admin)
            .HasForeignKey(g => g.AdminId)
            .OnDelete(DeleteBehavior.Cascade);

        var adminUser = new AUser
        {
            Id = "1",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            ConcurrencyStamp = "6e743e0c-96e7-4dce-a3fa-b5cefe2501d0",
            //P@ssw0rd
            PasswordHash = "AQAAAAEAACcQAAAAEJ8CkPskOQQUVMsWC4NgvCwi8YejKw25/cm/CuTQo/ji/yv3GniXVNSEA27c7Ab2QQ==",
            SecurityStamp = "f02e6368-af8a-4a09-bbd8-db5ed40749b5"
        };

        builder.HasData(adminUser);
    }
}
