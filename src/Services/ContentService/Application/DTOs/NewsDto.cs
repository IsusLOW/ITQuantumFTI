namespace ContentService.Application.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Head { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string>? ImageUrls { get; set; }
        public string? Author { get; set; }
    }
}
