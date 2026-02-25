using FluentValidation;
using MentorService.Application.DTOs;

namespace MentorService.Application.Validators
{
    public class UpdateMentorDtoValidator : AbstractValidator<UpdateMentorDto>
    {
        public UpdateMentorDtoValidator()
        {
            When(x => x.FirstName != null, () =>
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("First name cannot be empty.")
                    .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");
            });

            When(x => x.LastName != null, () =>
            {
                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Last name cannot be empty.")
                    .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");
            });
        }
    }
}
