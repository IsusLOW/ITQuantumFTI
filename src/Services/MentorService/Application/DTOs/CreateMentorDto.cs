using System.ComponentModel.DataAnnotations;

namespace MentorService.Application.DTOs
{
    public class CreateMentorDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? Avatar { get; set; }

        public string? Description { get; set; }
    }
}
