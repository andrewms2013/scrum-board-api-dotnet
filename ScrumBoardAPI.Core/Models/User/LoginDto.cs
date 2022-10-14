using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Core.Models.User;

public class LoginDto
{
    public string UserName { get; set; }

    [StringLength(15, ErrorMessage = "The minimal password length is {2}, the maximum length is {1}", MinimumLength = 6)]
    public string Password { get; set; }

    public LoginDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
