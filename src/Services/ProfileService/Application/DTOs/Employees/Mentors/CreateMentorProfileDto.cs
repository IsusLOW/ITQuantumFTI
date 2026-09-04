using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.DTOs.Employees.Mentors
{
    public class CreateMentorProfileDto
    {
        public Guid? AuthUserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
    }
}