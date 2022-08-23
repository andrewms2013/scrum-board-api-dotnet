namespace ScrumBoardAPI.Models.User;

public class CreateUserDto
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
