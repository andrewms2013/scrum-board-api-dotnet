namespace ScrumBoardAPI.Data;

public class Task
{
    public int Id { get; set; }

    public int WorkspaceId { get; set; }

    public Workspace Workspace { get; set; } = null!;

    public string CreatorId { get; set; } = null!;

    public AUser Creator { get; set; } = null!;

    public string? AssigneeId { get; set; }

    public AUser? Assignee { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Priority { get; set; } = null!;
}
