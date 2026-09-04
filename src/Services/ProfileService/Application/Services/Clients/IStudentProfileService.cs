using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.DTOs.Clients;
using ProfileService.Application.Services.Shared;

namespace ProfileService.Application.Services.Clients
{
    public interface IStudentProfileService 
        : IProfileService<StudentProfileDto,
          CreateStudentProfileDto,
          UpdateStudentProfileDto>
    {

    }
}