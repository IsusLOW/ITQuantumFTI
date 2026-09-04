using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Domain.Clients
{
    public class ClientProfile
    {
        public int Id { get; set; }
        public Guid? AuthUserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? School { get; set; }
        public ClientType ClientType { get; set; }

        public int? ParentProfileId { get; set; }
        public ClientProfile? ParentProfile { get; set; }
        public ICollection<ClientProfile> Children { get; set; } = [];

        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string GetFullName() => $"{LastName} {FirstName} {SecondName}";
    }
}