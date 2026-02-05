using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseService.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? WorkProg { get; set; } // учебная программа

        public List<string>? ImageUrls { get; set; } // картинки курса (JSONB в PostgreSQL)

        public int MentorId { get; set; }
    }
}