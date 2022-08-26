using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<ATask>
{
    public void Configure(EntityTypeBuilder<ATask> builder)
    {
        builder.HasData(
            new ATask
            {
                Id = 1,
                Name = "Task 1",
                Description = "Task 1 description",
                AssigneeId = "1",
                CreatorId = "1",
                WorkspaceId = 1,
                Priority = "High"
            },
            new ATask
            {
                Id = 2,
                Name = "Task 2",
                Description = "Task 2 description",
                AssigneeId = "1",
                CreatorId = "1",
                WorkspaceId = 1,
                Priority = "Medium"
            }
        );
    }
}
