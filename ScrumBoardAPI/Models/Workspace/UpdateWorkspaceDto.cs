namespace ScrumBoardAPI.Models.Workspace;

public class UpdateWorkspaceDto : BaseWorkspaceDto
{
    public int Id { get; set; }

    public UpdateWorkspaceDto(string name) : base(name)
    {
    }
}
