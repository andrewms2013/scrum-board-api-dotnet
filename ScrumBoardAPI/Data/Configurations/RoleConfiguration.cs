using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole {
                Id = "1",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "fa46e43a-7635-49cc-bc23-74e91748eb02"
            },
            new IdentityRole {
                Id = "2",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "3ef07262-ed9a-4cac-b3c4-ef26188c347f"
            }
        );
    }
}
