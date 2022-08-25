
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto) {
            var isValidUser = await _authManager.Login(loginDto);

            if (!isValidUser) {
                return Forbid();
            }

            return Ok();
        }
    }
}
