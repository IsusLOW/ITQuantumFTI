using FluentValidation;
using NewsService.Application.DTOs;

namespace NewsService.Application.Validators
{
    public class UpdateNewsDtoValidator : AbstractValidator<UpdateNewsDto>
    {
        public UpdateNewsDtoValidator()
        {
            RuleFor(x => x.Head)
                .NotEmpty().WithMessage("Head is required.")
                .MaximumLength(100).WithMessage("Head must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
