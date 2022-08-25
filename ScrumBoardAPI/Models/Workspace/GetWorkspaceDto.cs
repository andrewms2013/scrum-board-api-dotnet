namespace ScrumBoardAPI.Models.Workspace;

public class GetWorkspaceDto : BaseWorkspaceDto
{
    public int Id { get; set; }

    public GetWorkspaceDto(string name) : base(name)
    {
    }
}
