using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SliderService.Domain.Entities
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}