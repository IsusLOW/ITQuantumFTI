using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorService.Domain.Entities
{
    public class Mentor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string GetFullName() => $"{LastName} {FirstName} {SecondName}";
    }
}