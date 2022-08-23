using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumBoardAPI.Data;

public class AUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public IList<Workspace>? Workspaces { get; set; } = null!;

    public List<Task>? CreatedTasks { get; set; } = null!;

    public List<Task>? AssignedTasks { get; set; } = null!;

}
