using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.Workspace;

namespace ScrumBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Administrator")]
    [ApiController]
    public class WorkspaceController : BaseApplicationController
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IMapper _autoMapper;

        private readonly UserManager<AUser> _userManager;

        public WorkspaceController(IWorkspaceRepository workspaceRepository, IMapper autoMapper, UserManager<AUser> userManager)
        {
            this._workspaceRepository = workspaceRepository;
            this._autoMapper = autoMapper;
            this._userManager = userManager;
        }

        // GET: api/Workspace
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetWorkspaceDto>>> GetWorkspace()
        {
            var userId = GetUserId();
            var workspaces = await _workspaceRepository.GetWorkspacesByUserId(userId);

            if (workspaces is null)
            {
                return NotFound();
            }

            return _autoMapper.Map<List<GetWorkspaceDto>>(workspaces);
        }

        // GET: api/Workspace/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWorkspaceDetailsDto>> GetWorkspace(int id)
        {
            var userId = GetUserId();

            var canUserAccessWorkspace = await _workspaceRepository.CanUserAccessWorkspace(userId, id);

            if (!canUserAccessWorkspace) {
                return Forbid();
            }

            var workspace = await _workspaceRepository.GetWorkspaceWithDetails(id);

            if (workspace == null)
            {
                return NotFound();
            }

            var mapped = _autoMapper.Map<GetWorkspaceDetailsDto>(workspace);

            return mapped;
        }

        // PUT: api/Workspace/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> RenameWorkspace(int id, string newName)
        {
            var userId = GetUserId();

            var isWorkspaceAdmin = await _workspaceRepository.IsUserWorkspaceAdmin(userId, id);

            if (!isWorkspaceAdmin) {
                return Forbid();
            }

            var workspace = await _workspaceRepository.GetAsync(id);

            if (workspace == null)
            {
                return NotFound();
            }

            workspace.Name = newName;

            try
            {
                await _workspaceRepository.UpdateAsync(workspace);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkspaceExistsAsync(id))
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

        // PUT: api/Workspace/5/AddUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/AddUser")]
        public async Task<IActionResult> AddUserToWorkspace(int id, string userName)
        {
            var userId = GetUserId();

            var isWorkspaceAdmin = await _workspaceRepository.IsUserWorkspaceAdmin(userId, id);

            if (!isWorkspaceAdmin) {
                return Forbid();
            }

            var workspace = await _workspaceRepository.GetWorkspaceWithDetails(id);

            if (workspace == null)
            {
                return NotFound("Workspace was not found");
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound("User was not found");
            }

            workspace.Users.Add(user);

            try
            {
                await _workspaceRepository.UpdateAsync(workspace);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkspaceExistsAsync(id))
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

        [HttpPut("{id}/RemoveUser")]
        public async Task<IActionResult> RemovUserFromWorkspace(int id, string userName)
        {
            var userId = GetUserId();

            var isWorkspaceAdmin = await _workspaceRepository.IsUserWorkspaceAdmin(userId, id);

            if (!isWorkspaceAdmin) {
                return Forbid();
            }

            var workspace = await _workspaceRepository.GetWorkspaceWithDetails(id);

            if (workspace == null)
            {
                return NotFound("Workspace was not found");
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound("User was not found");
            }

            workspace.Users.Remove(user);

            try
            {
                await _workspaceRepository.UpdateAsync(workspace);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkspaceExistsAsync(id))
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

        // POST: api/Workspace
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetWorkspaceDto>> PostWorkspace(string name)
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var workspace = new Workspace(name, userId);
            workspace.Users = new List<AUser> {user};

            await _workspaceRepository.AddAsync(workspace);
            return CreatedAtAction("GetWorkspace", new { id = workspace.Id }, _autoMapper.Map<GetWorkspaceDto>(workspace));
        }

        // DELETE: api/Workspace/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace(int id)
        {
            var userId = GetUserId();

            var isWorkspaceAdmin = await _workspaceRepository.IsUserWorkspaceAdmin(userId, id);

            if (!isWorkspaceAdmin) {
                return Forbid();
            }

            var workspace = await _workspaceRepository.GetAsync(id);
            if (workspace == null)
            {
                return NotFound();
            }

            await _workspaceRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> WorkspaceExistsAsync(int id)
        {
            return await _workspaceRepository.Exists(id);
        }
    }
}
