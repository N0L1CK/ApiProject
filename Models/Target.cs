using ApiProject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Models
{
    public class Target
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20), NotNull]
        public string? TargetName { get; set; }
        [MaxLength(255)]
        public string? TargetText { get; set; }
        [Required]
        public EPriority Priority { get; set; } = EPriority.Low;

        [Required]
        public IEnumerable<TargetUser>? Users { get; set; } = new List<TargetUser>();

        [Required]
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
