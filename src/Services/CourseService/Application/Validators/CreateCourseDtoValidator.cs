using CourseService.Application.DTOs;
using FluentValidation;

namespace CourseService.Application.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название курса не может быть пустым.")
                .MaximumLength(100).WithMessage("Название курса не может превышать 100 символов.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание курса не может быть пустым.");
        }
    }
}
