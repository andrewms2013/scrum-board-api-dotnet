using ScrumBoardAPI.Models.Enums;

namespace ScrumBoardAPI.Data;

public class ATask
{
    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    private Workspace? _workspace;

    public Workspace Workspace
    {
        set => _workspace = value;
        get => _workspace
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Workspace));
    }

    public string CreatorId { get; set; }

    private AUser? _creator;

    public AUser Creator
    {
        set => _creator = value;
        get => _creator
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Creator));
    }

    public string? AssigneeId { get; set; }

    public AUser? Assignee { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ATaskPriority Priority { get; set; }

    public ATaskStatus Status { get; set; }

    public ATask(
        string name,
        string description,
        ATaskPriority priority,
        int workspaceId,
        string creatorId,
        ATaskStatus status
    )
    {
        Name = name;
        Description = description;
        Priority = priority;
        WorkspaceId = workspaceId;
        CreatorId = creatorId;
        Status = status;
    }
}
