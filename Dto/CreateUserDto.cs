using System.ComponentModel.DataAnnotations;

namespace InterviewManagementPortal.Models.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }
    }
}
