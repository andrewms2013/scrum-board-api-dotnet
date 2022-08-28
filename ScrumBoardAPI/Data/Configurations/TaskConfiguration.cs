using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrumBoardAPI.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<ATask>
{
    public void Configure(EntityTypeBuilder<ATask> builder)
    {
        builder.HasData(
            new ATask("Task 1", "Task 1 description", "High", 1, "1")
            {
                Id = 1,
                AssigneeId = "1",
            },
            new ATask("Task 2", "Task 2 description", "Medium", 1, "1")
            {
                Id = 2,
                AssigneeId = "1",
            }
        );
    }
}
