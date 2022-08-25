using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class CreateUserDto
{
    public string UserName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [StringLength(15, ErrorMessage = "The minimal password length is {2}, the maximum length is {1}", MinimumLength = 6)]
    public string Password { get; set; } = null!;
}
