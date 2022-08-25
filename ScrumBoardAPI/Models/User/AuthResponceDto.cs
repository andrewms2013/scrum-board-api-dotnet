using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class AuthResponceDto
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public AuthResponceDto(string userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}
