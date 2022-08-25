namespace ScrumBoardAPI.Models.Workspace;

public abstract class BaseWorkspaceDto
{
    public string Name { get; set; }

    protected BaseWorkspaceDto(string name)
    {
        Name = name;
    }
}
