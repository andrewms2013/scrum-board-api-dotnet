using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class GetUserDto
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public GetUserDto(string userId, string userName, string email)
    {
        UserName = userName;
        Email = email;
        Id = userId;
    }
}
