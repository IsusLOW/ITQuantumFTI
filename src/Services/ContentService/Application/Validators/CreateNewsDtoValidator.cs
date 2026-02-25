using ContentService.Application.DTOs;
using FluentValidation;

namespace ContentService.Application.Validators
{
    public class CreateNewsDtoValidator : FluentValidation.AbstractValidator<CreateNewsDto>
    {
        public CreateNewsDtoValidator()
        {
            RuleFor(x => x.Head)
                .NotEmpty().WithMessage("Head is required.")
                .MaximumLength(100).WithMessage("Head must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
