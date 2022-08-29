using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScrumBoardAPI.Data;
using ScrumBoardAPI.Models.Task;

namespace ScrumBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Administrator")]
    [ApiController]
    public class TaskController : BaseApplicationController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

        public TaskController(ITaskRepository taskRepository, IUserRepository userRepository, IMapper autoMapper)
        {
            _taskRepository = taskRepository;
            _autoMapper = autoMapper;
            _userRepository = userRepository;
        }

        // PUT: api/Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutATask(int id, PutTaskDto aTaskDto)
        {
            if (id != aTaskDto.Id)
            {
                return BadRequest();
            }

            var task = await _taskRepository.GetAsync(id);

            if (task == null) {
                return NotFound();
            }

            _autoMapper.Map(aTaskDto, task);

            try
            {
                await _taskRepository.UpdateAsync(task);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ATaskExists(id))
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

        // POST: api/Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetTaskDto>> PostATask(CreateTaskDto createTaskDto)
        {
            var userId = GetUserId();

            if (userId != createTaskDto.CreatorId) {
                return BadRequest();
            }

            var canUserAccessWorkspace = await _userRepository.CanUserAccessWorkspace(userId, createTaskDto.WorkspaceId);

            if (!canUserAccessWorkspace) {
                return Forbid();
            }

            var task = _autoMapper.Map<ATask>(createTaskDto);

            await _taskRepository.AddAsync(task);

            return Ok(_autoMapper.Map<GetTaskDto>(task));
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteATask(int id)
        {
            var userId = GetUserId();

            var task = await _taskRepository.GetAsync(id);

            if (task is null) {
                return NotFound();
            }

            var canUserAccessWorkspace = await _userRepository.CanUserAccessWorkspace(userId, task.WorkspaceId);

            if (!canUserAccessWorkspace) {
                return Forbid();
            }

            await _taskRepository.DeleteAsync(task.Id);

            return NoContent();
        }

        private async Task<bool> ATaskExists(int id)
        {
            return await _taskRepository.Exists(id);
        }
    }
}
