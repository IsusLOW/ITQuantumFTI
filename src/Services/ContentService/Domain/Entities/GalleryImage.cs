namespace ContentService.Domain.Entities
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Title { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
