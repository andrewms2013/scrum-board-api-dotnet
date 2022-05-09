namespace ScrumBoardAPI.Data;

public class WorkspaceUser
{
    public string UserId = null!;
    public AUser ApplicationUser { get; set; } = null!;
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = null!;
}
