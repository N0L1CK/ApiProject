using ApiProject.Data;
using ApiProject.Interfaces;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Linq;

namespace ApiProject.Repositoryes
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            if (entity == null) 
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Add(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            if (entity == null) 
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectName(string name)
        {
            return await _context.Projects.Where(e=>e.ProjectName.StartsWith(name)).ToListAsync();
        }

        public async Task<IEnumerable<Target>> GetTargetsAsync(int id)
        {
            return await _context.Targets.Where(e=>e.ProjectId == id).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserName(string name)
        {
            return await _context.Users.Where(e => e.Name.StartsWith(name) 
            || e.LastName.StartsWith(name)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersInProjectAsync(int id)
        {
            return await _context.ProjectUsers.Where(t=>t.ProjectId == id).Select(e=>e.User).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersInTargetAsync(int id)
        {
            return (IEnumerable<User>)await _context.Targets.Where(e => e.Id == id).Select(t=>t.Users).ToListAsync();
        }

        public async Task<Login> LogInUser(string login, string password)
        {
            return await _context.Logins.FirstOrDefaultAsync(e=>e.Email == login && e.Password == password);
        }

        public Task<Project> AddUserToProject(int userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> AddUserToTarget(int userId, int targetId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> AddTargetToProject(int userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserId(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<bool> CheckEmail(string email) 
        {
            var temp = await _context.Logins.Where(e => e.Email == email).ToListAsync();
            return temp.Count > 0;
        }

        public async Task<Login> GetLogIUser(User user)
        {
            return await _context.Logins.FirstOrDefaultAsync(e=>e.UserId == user.Id);
        }

        public async Task<Project> GetProjectId(int Id)
        {
            return await _context.Projects.FirstOrDefaultAsync(e => e.Id == Id);
        }
    }
}
