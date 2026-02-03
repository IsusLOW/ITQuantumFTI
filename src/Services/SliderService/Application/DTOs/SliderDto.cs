using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SliderService.Application.DTOs
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}