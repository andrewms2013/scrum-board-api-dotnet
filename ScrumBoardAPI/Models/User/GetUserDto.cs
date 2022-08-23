namespace ScrumBoardAPI.Models.User;

public class GetUserDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public GetUserDto(string id, string name, string email, string passwordHash)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}
