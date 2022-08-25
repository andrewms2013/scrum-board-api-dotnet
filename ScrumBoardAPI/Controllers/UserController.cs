using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.User;

namespace ScrumBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepostory _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepostory userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAUser()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<GetUserDto>>(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetAUser(string id)
        {
            var aUser = await _userRepository.GetAsync(id);

            if (aUser == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetUserDto>(aUser);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAUser(string id, UpdateUserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetAsync(id);

            if (user is null) {
                return NotFound();
            }

            _mapper.Map(userDto, user);

            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AUserExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AUser>> PostAUser(CreateUserDto userDto)
        {
            var aUser = _mapper.Map<AUser>(userDto);

            try
            {
                await _userRepository.AddAsync(aUser);
            }
            catch (DbUpdateException)
            {
                if (await AUserExistsAsync(aUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAUser", new { id = aUser.Id }, aUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAUser(string id)
        {
            var aUser = await _userRepository.GetAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> AUserExistsAsync(string id)
        {
            return await _userRepository.Exists(id);
        }
    }
}
