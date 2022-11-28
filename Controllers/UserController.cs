using ApiProject.Interfaces;
using ApiProject.Models;
using ApiProject.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ApiProject.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllUsersAsync();

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
                var result = await _repository.GetUserName(name.ToUpper());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(string name, string lastName, string email, 
            string password, ERole role = ERole.User, string photo = "../Assets/user.png")
        {
          
            if (!await _repository.CheckEmail(email.ToUpper()))
            {
                User user = new()
                {
                    Name = name.ToUpper(),
                    LastName = lastName.ToUpper(),
                    Login = new() { Email = email.ToUpper(), Role = role, Password = ConvertPassword(password) },
                    Photo = photo,
                    Projects = new List<ProjectUser>(),
                    Targets = new List<TargetUser>(),

                };

                try
                {
                    _repository.Add(user);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(user.Id);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
            return BadRequest(); 
            
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var user = await _repository.GetUserId(Id);
                if (user == null) return NotFound();

                _repository.Delete(user);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Deleted: {Id}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            return BadRequest();
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, string? name, string? lastName, string? email,
            string? password, ERole? role, string? photo = "../Assets/user.png")
        {
            if (email == null || !await _repository.CheckEmail(email.ToUpper()))
            {
                try
                {
                    var user = await _repository.GetUserId(Id);
                    if (user == null) return NotFound();
                    var login = await _repository.GetLogIUser(user);
                    if (login == null) return NotFound();
                    user.Name = name.ToUpper() ?? user.Name;
                    user.LastName = lastName.ToUpper() ?? user.LastName;
                    user.Photo = photo ?? user.Photo;
                    if (password != null) 
                    {
                        login.Password = ConvertPassword(password);
                    }
                    if (email != null) 
                    {
                        login.Email = email.ToUpper();
                    }
                    if (role != null) 
                    {
                        login.Role = (ERole)role;
                    }
                    
                    _repository.Update(user);
                    _repository.Update(login);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok($"User update: {user.Id}");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }

            return BadRequest();
        }

        [NonAction]
        public string ConvertPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        }
    }
    
}
