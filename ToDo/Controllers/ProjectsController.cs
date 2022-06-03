using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Repository;
using Task = ToDo.Models.Task;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepositoryProject _projectRepository;
        private readonly IRepositoryTask _taskRepository;

        public ProjectsController(IRepositoryProject projectRepository, IRepositoryTask taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _projectRepository.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            var result = await _projectRepository.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Add(Project project)
        {
            if (project == null)
                return BadRequest();
            var createdProject = await _projectRepository.Create(project);
            return CreatedAtAction(nameof(Get), new {id = createdProject.Id}, createdProject);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Project>> Update(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var createdProject = await _projectRepository.Update(project);
            return CreatedAtAction(nameof(Get), new {id = createdProject.Id}, createdProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteProject = await _projectRepository.Get(id);
            if (deleteProject == null)
            {
                return NotFound("Not Found");
            }

            await _projectRepository.Delete(id);
            return Ok();
        }


        [HttpGet("{projectId:int}/tasks")]
        public async Task<ActionResult> GetTasksProject(int projectId)
        {
            return Ok(await _taskRepository.GetAllById(projectId));
        }

        [HttpGet("{projectId:int}/tasks/{id:int}")]
        public async Task<ActionResult<Task>> GetTask(int projectId, int id)
        {
            var result = await _taskRepository.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        //Create Task for project
        [HttpPost("{projectId:int}/tasks/")]
        public async Task<ActionResult> AddTaskProject(int projectId, Task task)
        {
            if (task == null)
                return BadRequest();
            var createdProject = await _taskRepository.Create(task);
            return CreatedAtAction(nameof(GetTask), new {projectId = task.ProjectId, id = task.Id}, task);
        }

        [HttpPut("{projectId:int}/tasks/{id:int}")]
        public async Task<ActionResult<Project>> UpdateTask(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var createdProject = await _projectRepository.Update(project);
            return CreatedAtAction(nameof(Get), new {id = createdProject.Id}, createdProject);
        }

        [HttpDelete("{projectId}/tasks/{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var deleteProject = await _taskRepository.Get(id);
            if (deleteProject == null)
            {
                return NotFound("Not Found");
            }

            await _taskRepository.Delete(id);
            return Ok();
        }
    }
}