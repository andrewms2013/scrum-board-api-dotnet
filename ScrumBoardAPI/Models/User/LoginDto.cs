using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class LoginDto
{
    public string UserName { get; set; } = null!;

    [StringLength(15, ErrorMessage = "The minimal password length is {2}, the maximum length is {1}", MinimumLength = 6)]
    public string Password { get; set; } = null!;
}
