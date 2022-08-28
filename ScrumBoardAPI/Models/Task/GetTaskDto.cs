using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Data;

public class GetTaskDto
{
    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    private GetUserDto? _creator;

    public GetUserDto Creator
    {
        set => _creator = value;
        get => _creator
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Creator));
    }

    public GetUserDto? Assignee { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Priority { get; set; }

    public GetTaskDto(
        string name,
        string description,
        string priority,
        int workspaceId
    )
    {
        Name = name;
        Description = description;
        Priority = priority;
        WorkspaceId = workspaceId;
    }
}
