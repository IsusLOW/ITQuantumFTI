using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseService.Application.DTOs
{
    public class CreateCourseDto
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? WorkProg { get; set; } // учебная программа

        public List<string>? ImageUrls { get; set; } // картинки курса (JSONB в PostgreSQL)
    }
}