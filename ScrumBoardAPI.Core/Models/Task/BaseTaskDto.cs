using ScrumBoardAPI.Data.Enums;

namespace ScrumBoardAPI.Core.Models.Task;

public class BaseTaskDto
{
    public int WorkspaceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ATaskPriority Priority { get; set; }

    public ATaskStatus Status { get; set; }

    public string CreatorId { get; set; }

    public string? AssigneeId { get; set; }

    public BaseTaskDto(
        string name,
        string description,
        ATaskPriority priority,
        int workspaceId,
        ATaskStatus status,
        string creatorId
    )
    {
        Name = name;
        Description = description;
        Priority = priority;
        WorkspaceId = workspaceId;
        Status = status;
        CreatorId = creatorId;
    }
}
