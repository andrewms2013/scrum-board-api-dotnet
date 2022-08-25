using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.Workspace;

namespace ScrumBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IMapper _autoMapper;

        public WorkspaceController(IWorkspaceRepository workspaceRepository, IMapper autoMapper)
        {
            this._workspaceRepository = workspaceRepository;
            this._autoMapper = autoMapper;
        }

        // GET: api/Workspace
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetWorkspaceDto>>> GetWorkspace()
        {
            var workspaces = await _workspaceRepository.GetAllAsync();
            return _autoMapper.Map<List<GetWorkspaceDto>>(workspaces);
        }

        // GET: api/Workspace/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWorkspaceDto>> GetWorkspace(int id)
        {
            var workspace = await _workspaceRepository.GetAsync(id);

            if (workspace == null)
            {
                return NotFound();
            }

            return _autoMapper.Map<GetWorkspaceDto>(workspace);
        }

        // PUT: api/Workspace/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkspace(int id, UpdateWorkspaceDto updateWorkspaceDto)
        {
            if (id != updateWorkspaceDto.Id)
            {
                return BadRequest();
            }

            var workspace = await _workspaceRepository.GetAsync(id);

            if (workspace == null)
            {
                return NotFound();
            }

            _autoMapper.Map(updateWorkspaceDto, workspace);

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
        public async Task<ActionResult<Workspace>> PostWorkspace(CreateWorkspaceDto createWorkspaceDto)
        {

            var workspace = _autoMapper.Map<Workspace>(createWorkspaceDto);
            await _workspaceRepository.AddAsync(workspace);
            return CreatedAtAction("GetWorkspace", new { id = workspace.Id }, workspace);
        }

        // DELETE: api/Workspace/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace(int id)
        {
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
