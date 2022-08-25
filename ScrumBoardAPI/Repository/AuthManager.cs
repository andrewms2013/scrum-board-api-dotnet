using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<AUser> _userManager;

    public AuthManager(IMapper mapper, UserManager<AUser> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user is null)
        {
            return false;
        }

        bool isValidCredentials = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        return isValidCredentials;
    }

    public async Task<IEnumerable<IdentityError>> Register(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<AUser>(createUserDto);

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (result.Succeeded) {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }
}
