namespace ContentService.Application.DTOs
{
    public class GalleryImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Title { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
