using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AUser> _userManager;

    public AuthManager(IMapper mapper, IConfiguration configuration, UserManager<AUser> userManager)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._userManager = userManager;
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

        return new AuthResponceDto(user.Id, token);
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
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }.Union(roleClaims).Union(userClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSetting:Issuer"],
            audience: _configuration["JwtSetting:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSetting:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
