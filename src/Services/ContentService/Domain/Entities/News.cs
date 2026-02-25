namespace ContentService.Domain.Entities
{
    public class News
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Head { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string>? ImageUrls { get; set; }
        public string? Author { get; set; }
    }
}
