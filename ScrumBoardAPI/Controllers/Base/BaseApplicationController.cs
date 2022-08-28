using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using ScrumBoardAPI.Exceptions;

public abstract class BaseApplicationController : ControllerBase
{
    protected string GetUserId()
    {
        var user = User.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.NameId);
        if (user is null) {
            throw new NotFoundException("NameId claim was not found");
        }
        return user.Value;
    }
}
