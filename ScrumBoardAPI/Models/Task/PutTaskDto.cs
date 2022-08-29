using ScrumBoardAPI.Models.Enums;

namespace ScrumBoardAPI.Models.Task;

public class PutTaskDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ATaskPriority? Priority { get; set; }

    public ATaskStatus? Status { get; set; }

    public string? AssigneeId { get; set; }
}
