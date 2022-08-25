using Microsoft.AspNetCore.Identity;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(CreateUserDto createUserDto);

    Task<bool> Login(LoginDto loginDto);
}
