using Microsoft.AspNetCore.Identity;

namespace ScrumBoardAPI.Data;

public class AUser : IdentityUser
{
    private IList<Workspace>? _workspaces;

    public IList<Workspace> Workspaces
    {
        set => _workspaces = value;
        get => _workspaces
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Workspaces));
    }

    private IList<Workspace>? _administratedWorkspaces;

    public IList<Workspace> AdministratedWorkspaces
    {
        set => _administratedWorkspaces = value;
        get => _administratedWorkspaces
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(AdministratedWorkspaces));
    }

    private IList<ATask>? _createdTasks;

    public IList<ATask> CreatedTasks
    {
        set => _createdTasks = value;
        get => _createdTasks
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(CreatedTasks));
    }

    private IList<ATask>? _assignedTasks;

    public IList<ATask> AssignedTasks
    {
        set => _assignedTasks = value;
        get => _assignedTasks
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(AssignedTasks));
    }
}
