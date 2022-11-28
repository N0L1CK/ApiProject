using ApiProject.Data;
using ApiProject.Interfaces;
using ApiProject.Models;
using ApiProject.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace ApiProject.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IRepository _repository;

        public ProjectsController(IRepository repository)
        {
            _repository = repository;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllProjectsAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("user/")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var project = await _repository.GetProjectId(Id);
                var result = await _repository.GetUsersInProjectAsync(Id);
                if (project == null || result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetName(string name)
        {
            try
            {
                var result = await _repository.GetProjectName(name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(string projectName, string projectText,
            DateTime endDate, int authorId, DateTime dateStart, EPriority priority = 0)
        {
            Project project = new()
            {
                ProjectName = projectName.ToUpper(),
                ProjectText = projectText,
                EndDate = endDate <= DateTime.Now ? endDate.AddDays(7) : endDate,
                AuthorId = authorId > 0? 1: authorId,
                Priority = priority,
                StartDate = dateStart < DateTime.Now ? DateTime.Now : dateStart,
                Users = new List<ProjectUser>(),
                Targets= new List<Target>(),
            };
            try
            {
                _repository.Add(project);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(project.Id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            
            return BadRequest();

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var project = await _repository.GetProjectId(Id);
                if (project == null) return NotFound();

                _repository.Delete(project);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Deleted: {Id} project");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            return BadRequest();
        }
        [HttpPost("addUser/{projectId}/{userId}")]
        public async Task<IActionResult> AddUserToProject(int userId, int projectId) 
        {
            try
            {
                var user = await _repository.GetUserId(userId);
                var project = await _repository.GetProjectId(projectId);
                if (project == null && user == null) return NotFound();
                
                
                ProjectUser projectUser = new ProjectUser() 
                {
                    UserId = userId,
                    ProjectId = projectId,
                };

                _repository.Add(projectUser);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"user {userId} add to project {projectId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            return BadRequest();
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, string? name, string? text, DateTime? start,
            DateTime? end, EPriority? priority, int? author)
        {

            try
            {
                var project = await _repository.GetProjectId(Id);
                if (project == null) return NotFound();

                project.ProjectName = name.ToUpper() ?? project.ProjectName;
                project.ProjectText = text ?? project.ProjectText;
                project.Priority = priority ?? project.Priority;
                project.AuthorId = author ?? project.AuthorId;

                if (start == null || start < DateTime.Now)
                {
                    project.StartDate = DateTime.Now;
                }
                else 
                {
                    project.StartDate = start;
                }
                if (end == null || end <= DateTime.Now)
                {
                    project.EndDate = DateTime.Now.AddDays(7);
                }
                else 
                {
                    project.EndDate = end;
                }

                _repository.Update(project);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Project update: {project.Id}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            

            return BadRequest();
        }
    }
}
