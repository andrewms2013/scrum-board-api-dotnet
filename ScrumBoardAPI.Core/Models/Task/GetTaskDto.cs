using ScrumBoardAPI.Data.Enums;
using ScrumBoardAPI.Core.Models.User;

namespace ScrumBoardAPI.Core.Models.Task;

public class GetTaskDto: BaseTaskDto
{
    public int Id { get; set; }

    private GetUserDto? _creator;

    public GetUserDto Creator
    {
        set => _creator = value;
        get => _creator
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Creator));
    }

    public GetUserDto? Assignee { get; set; }

    public GetTaskDto(
        string name,
        string description,
        ATaskPriority priority,
        int workspaceId,
        ATaskStatus status,
        string creatorId
    ): base(name, description, priority, workspaceId, status, creatorId) {}
}
