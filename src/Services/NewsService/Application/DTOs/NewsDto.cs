using System;
using System.Collections.Generic;

namespace NewsService.Application.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Head { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IReadOnlyList<string> ImageUrls { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
