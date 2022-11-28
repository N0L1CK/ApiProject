using Microsoft.EntityFrameworkCore;

namespace ApiProject.Models
{
    [Keyless]
    public class TargetUser
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TargetId { get; set; }
        public Target? Target { get; set; }
    }
}
