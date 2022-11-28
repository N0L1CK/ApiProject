using Microsoft.EntityFrameworkCore;

namespace ApiProject.Models
{
    [Keyless]
    public class ProjectUser
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
