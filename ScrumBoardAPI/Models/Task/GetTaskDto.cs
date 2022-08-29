using ScrumBoardAPI.Models.Enums;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Data;

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
