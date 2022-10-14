namespace ScrumBoardAPI.Data;

public class Workspace
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string AdminId { get; set; }

    private AUser? _admin;

    public AUser Admin
    {
        set => _admin = value;
        get => _admin
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Users));
    }

    private IList<AUser>? _users;

    public IList<AUser> Users
    {
        set => _users = value;
        get => _users
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Users));
    }

    private IList<ATask>? _tasks;

    public IList<ATask> Tasks
    {
        set => _tasks = value;
        get => _tasks
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Tasks));
    }

    public Workspace(
        string name,
        string adminId
    )
    {
        Name = name;
        AdminId = adminId;
    }
}
