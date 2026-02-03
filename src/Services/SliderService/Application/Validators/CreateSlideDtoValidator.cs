using FluentValidation;
using SliderService.Application.DTOs;

namespace SliderService.Application.Validators
{
    public class CreateSlideDtoValidator : AbstractValidator<CreateSlideDto>
    {
        public CreateSlideDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.ImageUrl)).WithMessage("Image URL must be a valid URL.");
        }
    }
}