using ScrumBoardAPI.Data.Enums;

namespace ScrumBoardAPI.Core.Models.Task;

public class CreateTaskDto : BaseTaskDto
{
    public CreateTaskDto(
        string name,
        string description,
        ATaskPriority priority,
        int workspaceId,
        ATaskStatus status,
        string creatorId
    ) : base(name, description, priority, workspaceId, status, creatorId)
    {
    }
}
