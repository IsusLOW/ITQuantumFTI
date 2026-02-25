namespace MentorService.Application.DTOs
{
    public class MentorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? Description { get; set; }
    }
}
