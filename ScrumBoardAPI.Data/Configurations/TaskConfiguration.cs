using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrumBoardAPI.Data.Enums;

namespace ScrumBoardAPI.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<ATask>
{
    public void Configure(EntityTypeBuilder<ATask> builder)
    {
        builder.HasData(
            new ATask("Task 1", "Task 1 description", ATaskPriority.High, 1, "1", ATaskStatus.ToDo)
            {
                Id = 1,
                AssigneeId = "1",
            },
            new ATask("Task 2", "Task 2 description", ATaskPriority.Medium, 1, "1", ATaskStatus.InProgress)
            {
                Id = 2,
                AssigneeId = "1",
            }
        );
    }
}
