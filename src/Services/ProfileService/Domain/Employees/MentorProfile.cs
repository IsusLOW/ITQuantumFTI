using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Domain.Employees
{
    public class MentorProfile
    {
        public int Id { get; set; }
        public Guid? AuthUserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string GetFullName() => $"{LastName} {FirstName} {SecondName}";
    }
}