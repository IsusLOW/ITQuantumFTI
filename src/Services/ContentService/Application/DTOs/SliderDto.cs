namespace ContentService.Application.DTOs
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
