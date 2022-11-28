using ApiProject.Interfaces;
using ApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ApiProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository _repository;

        public LoginController(IRepository repository)
        {
            _repository = repository;

        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<Login> GetLogin(string login, string password)
        {
            return await _repository.LogInUser(login.ToUpper(), ConvertPassword(password));
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
