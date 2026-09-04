using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ProfileService.Application.DTOs.Clients;

namespace ProfileService.Application.Validators.Clients
{
    public class CreateParentProfileDtoValidator : AbstractValidator<CreateParentProfileDto>
    {
        public CreateParentProfileDtoValidator()
        {
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
        }
    }
}