using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30), NotNull]
        public string? Name { get; set; }
        [Required, MaxLength(30), NotNull]
        public string? LastName { get; set; }
        [MaxLength(100), MaybeNull]
        public string? Photo { get; set; } = "../Assets/user.png";
        [Required]
        public IEnumerable<ProjectUser>? Projects { get; set; } = new List<ProjectUser>();
        [Required]
        public IEnumerable<TargetUser>? Targets { get; set; } = new List<TargetUser>();
        [Required]
        public Login? Login { get; set; }

    }
}
