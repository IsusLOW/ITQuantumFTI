using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.DTOs.Employees.Mentors;
using ProfileService.Application.Services.Shared;

namespace ProfileService.Application.Services.Employees
{
    public interface IMentorProfileService 
        : IProfileService<MentorProfileDto,
          CreateMentorProfileDto,
          UpdateMentorProfileDto>  
    {

    }
}