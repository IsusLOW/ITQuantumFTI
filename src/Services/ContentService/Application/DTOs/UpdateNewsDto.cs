namespace ContentService.Application.DTOs
{
    public class UpdateNewsDto
    {
        public string Head { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string>? ImageUrls { get; set; }
        public string? Author { get; set; }
    }
}
