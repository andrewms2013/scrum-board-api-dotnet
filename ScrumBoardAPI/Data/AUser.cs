using Microsoft.AspNetCore.Identity;

namespace ScrumBoardAPI.Data;

public class AUser : IdentityUser
{
    public IList<Workspace>? Workspaces { get; set; } = null!;

    public IList<Workspace>? AdministratedWorkspaces { get; set; } = null!;

    public List<ATask>? CreatedTasks { get; set; } = null!;

    public List<ATask>? AssignedTasks { get; set; } = null!;
}
