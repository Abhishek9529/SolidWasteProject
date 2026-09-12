namespace InterviewManagementPortal.Models.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? Department { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
