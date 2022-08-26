
using Microsoft.AspNetCore.Mvc;
using ScrumBoardAPI.Contracts;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] CreateUserDto createUserDto) {
            var errors = await _authManager.Register(createUserDto);

            if (errors.Any()) {
                foreach (var error in errors) {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

          // POST: api/Auth/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponceDto>> Login([FromBody] LoginDto loginDto) {
            var authResponce = await _authManager.Login(loginDto);

            if (authResponce is null) {
                return Unauthorized();
            }

            return Ok(authResponce);
        }

          // POST: api/Auth/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponceDto request) {
            var authResponce = await _authManager.VerifyRefreshToken(request);

            if (authResponce is null) {
                return Unauthorized();
            }

            return Ok(authResponce);
        }
    }
}
