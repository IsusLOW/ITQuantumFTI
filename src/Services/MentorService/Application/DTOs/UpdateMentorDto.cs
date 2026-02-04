using System.ComponentModel.DataAnnotations;

namespace MentorService.Application.DTOs
{
    public class UpdateMentorDto
    {
        [MinLength(1)]
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [MinLength(1)]
        public string? LastName { get; set; }

        public string? Avatar { get; set; }

        public string? Description { get; set; }
    }
}
