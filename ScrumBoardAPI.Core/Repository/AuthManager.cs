using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScrumBoardAPI.Core.Contracts;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Core.Models.User;

namespace ScrumBoardAPI.Core.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AUser> _userManager;

    private const string _loginProvider = "ScrumBoardAPI";

    private const string _refreshToken = "RefreshToken";

    public AuthManager(IMapper mapper, IConfiguration configuration, UserManager<AUser> userManager)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._userManager = userManager;
    }

    public async Task<string> CreateRefreshToken(AUser user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, _loginProvider, _refreshToken);
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, _loginProvider, _refreshToken);
        var result = await _userManager.SetAuthenticationTokenAsync(user, _loginProvider, _refreshToken, newRefreshToken);

        return newRefreshToken;
    }

    public async Task<AuthResponceDto?> VerifyRefreshToken(AuthResponceDto requestDto)
    {
        var user = await _userManager.FindByIdAsync(requestDto.UserId);

        if(user is null) {
            return null;
        }

        var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user, _loginProvider, _refreshToken, requestDto.RefreshToken);

        if (isValidRefreshToken) {
            var token = await GenerateToken(user);
            var refreshToken = await CreateRefreshToken(user);

            return new AuthResponceDto(user.Id, token, refreshToken);
        }

        await _userManager.UpdateSecurityStampAsync(user);

        return null;
    }

    public async Task<AuthResponceDto?> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        bool isValidCredentials = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (user is null || !isValidCredentials)
        {
            return null;
        }

        var token = await GenerateToken(user);
        var refreshToken = await CreateRefreshToken(user);

        return new AuthResponceDto(user.Id, token, refreshToken);
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

    private async Task<string> GenerateToken(AUser user)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id)
        }.Union(roleClaims).Union(userClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
