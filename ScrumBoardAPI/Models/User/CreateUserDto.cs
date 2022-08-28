using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class CreateUserDto
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [StringLength(15, ErrorMessage = "The minimal password length is {2}, the maximum length is {1}", MinimumLength = 6)]
    public string Password { get; set; }

    public CreateUserDto(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}
