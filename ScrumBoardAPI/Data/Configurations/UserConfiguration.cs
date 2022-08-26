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
        };

        PasswordHasher<AUser> ph = new PasswordHasher<AUser>();
        adminUser.PasswordHash = ph.HashPassword(adminUser, "P@ssw0rd");

        builder.HasData(adminUser);
    }
}
