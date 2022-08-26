namespace ScrumBoardAPI.Data;

public class Workspace
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string AdminId { get; set; } = null!;

    public AUser Admin { get; set; } = null!;

    public IList<AUser>? Users { get; set; } = null!;
}
