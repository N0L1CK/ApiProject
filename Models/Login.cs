using ApiProject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Models
{

    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required, NotNull, MaxLength(255)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, MaxLength(20), NotNull]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, NotNull]
        public ERole Role { get; set; }
        [Required]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
