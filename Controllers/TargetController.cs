using ApiProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        private readonly IRepository _repository;

        public TargetController(IRepository repository)
        {
            _repository = repository;

        }
        [HttpGet]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = await _repository.GetTargetsAsync(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("/user")]
        public async Task<IActionResult> GetUser(int Id)
        {
            try
            {
                var result = await _repository.GetUsersInTargetAsync(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
