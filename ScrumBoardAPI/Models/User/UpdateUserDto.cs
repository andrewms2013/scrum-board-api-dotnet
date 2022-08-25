namespace ScrumBoardAPI.Models.User;

public class UpdateUserDto
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; } = null;

    public string? Email { get; set; } = null;

    public string? PasswordHash { get; set; } = null;
}
