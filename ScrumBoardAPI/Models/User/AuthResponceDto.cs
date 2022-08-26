using System.ComponentModel.DataAnnotations;

namespace ScrumBoardAPI.Models.User;

public class AuthResponceDto
{
    public string UserId { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }

    public AuthResponceDto(string userId, string token, string refreshToken)
    {
        UserId = userId;
        Token = token;
        RefreshToken = refreshToken;
    }
}
