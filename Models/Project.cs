using ApiProject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20), NotNull]
        public string? ProjectName { get; set; }
        [MaxLength(255)]
        public string? ProjectText { get; set; }
        [Required, NotNull, DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; } = DateTime.Today;
        [Required, NotNull, DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        [Required, NotNull]
        public EPriority? Priority { get; set; } = EPriority.Low;
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public IEnumerable<ProjectUser>? Users { get; set; } = new List<ProjectUser>();
        [Required]
        public IEnumerable<Target>? Targets { get; set; } = new List<Target>();

    }
}
