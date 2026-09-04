using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Domain.Clients;

namespace ProfileService.Application.DTOs.Clients
{
    public class CreateParentProfileDto
    {
        public Guid AuthUserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
    }
}