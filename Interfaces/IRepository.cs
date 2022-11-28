using ApiProject.Models;

namespace ApiProject.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();



        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<Project>> GetAllProjectsAsync();


        Task<IEnumerable<Target>> GetTargetsAsync(int id);
        Task<IEnumerable<User>> GetUsersInProjectAsync(int id);
        Task<IEnumerable<User>> GetUsersInTargetAsync(int id);

        Task<Login> LogInUser(string login, string password);
        Task<Login> GetLogIUser(User user);
        Task<User> GetUserId(int id);
        Task<IEnumerable<User>> GetUserName(string name);
        Task<IEnumerable<Project>> GetProjectName(string name);
        Task<Project> GetProjectId(int Id);
        Task<Project> AddUserToProject(int userId, int projectId);
        Task<Project> AddUserToTarget(int userId, int targetId);
        Task<Project> AddTargetToProject(int userId, int projectId);

        Task<bool> CheckEmail(string email);

    }
}
