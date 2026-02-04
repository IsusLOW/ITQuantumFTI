using FluentValidation;
using MentorService.Application.DTOs;

namespace MentorService.Application.Validators
{
    public class CreateMentorDtoValidator : AbstractValidator<CreateMentorDto>
    {
        public CreateMentorDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");
        }
    }
}
