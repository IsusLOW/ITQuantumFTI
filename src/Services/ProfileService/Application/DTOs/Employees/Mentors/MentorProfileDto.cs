using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.DTOs.Employees.Mentors
{
    public class MentorProfileDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
    }
}