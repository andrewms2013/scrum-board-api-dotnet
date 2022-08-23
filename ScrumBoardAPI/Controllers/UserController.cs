using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly ScrumBoardDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ScrumBoardDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAUser()
        {
            if (_context.AUser == null)
            {
                return NotFound();
            }
            var users = await _context.AUser.ToListAsync();
            return _mapper.Map<List<GetUserDto>>(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetAUser(string id)
        {
            if (_context.AUser == null)
            {
                return NotFound();
            }
            var aUser = await _context.AUser.FindAsync(id);

            if (aUser == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetUserDto>(aUser);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAUser(string id, AUser aUser)
        {
            if (id != aUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(aUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AUserExists(id))
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
            if (_context.AUser == null)
            {
                return Problem("Entity set 'ScrumBoardDbContext.AUser'  is null.");
            }

            var aUser = _mapper.Map<AUser>(userDto);

            _context.AUser.Add(aUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AUserExists(aUser.Id))
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
            if (_context.AUser == null)
            {
                return NotFound();
            }
            var aUser = await _context.AUser.FindAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }

            _context.AUser.Remove(aUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AUserExists(string id)
        {
            return (_context.AUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
