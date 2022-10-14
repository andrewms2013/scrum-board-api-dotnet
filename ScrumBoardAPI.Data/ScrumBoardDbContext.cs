using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
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

    public DbSet<ATask> ATask => Set<ATask>();

    public DbSet<Workspace> Workspace => Set<Workspace>();

}

public class ScrumBoardDbContextFactory : IDesignTimeDbContextFactory<ScrumBoardDbContext>
{
    public ScrumBoardDbContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ScrumBoardDbContext>();
        var conn = configuration.GetConnectionString("ScrumBoardAPIDbServerConnectionString");
        optionsBuilder.UseSqlServer(conn);
        return new ScrumBoardDbContext(optionsBuilder.Options);
    }
}
