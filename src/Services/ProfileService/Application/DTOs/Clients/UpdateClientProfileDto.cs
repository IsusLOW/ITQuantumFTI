using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.DTOs.Clients
{
    public abstract class UpdateClientProfileDto
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? School { get; set; }
        public DateTime Birthday { get; set; }
    }
}