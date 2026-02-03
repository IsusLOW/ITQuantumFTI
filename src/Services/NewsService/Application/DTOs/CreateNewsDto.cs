using System.Collections.Generic;

namespace NewsService.Application.DTOs
{
    public class CreateNewsDto
    {
        public string Head { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IReadOnlyList<string> ImageUrls { get; set; } = null!;
    }
}
