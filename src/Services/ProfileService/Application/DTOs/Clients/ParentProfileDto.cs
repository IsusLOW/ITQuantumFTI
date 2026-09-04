using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.DTOs.Clients
{
    public class ParentProfileDto
    {
        public int Id { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<StudentProfileDto> Children { get; set; } = [];
        public DateTime Birthday { get; set; }
    }
}