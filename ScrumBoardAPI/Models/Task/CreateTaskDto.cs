using ScrumBoardAPI.Models.Enums;

namespace ScrumBoardAPI.Data;

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
