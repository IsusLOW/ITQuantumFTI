using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.DTOs.Clients
{
    public class CreateStudentProfileDto
    {
        public Guid? AuthUserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? School { get; set; }
        public int? ParentProfileId { get; set; }
        public DateTime Birthday { get; set; }
    }
}