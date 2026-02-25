using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CourseService.Application.DTOs;
using CourseService.Application.Repositories;
using CourseService.Application.Services;
using CourseService.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace CourseService.Application.Services
{
    public class CourseAppService(
        ICourseRepository repository, 
        IMapper mapper, 
        ILogger<CourseAppService> logger, 
        IValidator<CreateCourseDto> createValidator, 
        IValidator<UpdateCourseDto> updateValidator) : ICourseAppService
    {
 
        public async Task<IReadOnlyList<CourseDto>> GetCoursesAsync()
        {
            logger.LogInformation("Getting all courses");
            var courses = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            logger.LogInformation("Getting course with {CourseId}", id);
            var course = await repository.GetByIdAsync(id);
            if (course == null)
            {
                logger.LogWarning("Course with ID {CourseId} not found.", id);
                throw new NotFoundException(nameof(Course), id);
            }
            return mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            logger.LogDebug("Validating new course");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating course: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new course");
            var course = mapper.Map<Course>(dto);
            await repository.AddAsync(course);
            
            logger.LogInformation("Successfully created course with ID {CourseId}", course.Id);
            return mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto dto)
        {
            logger.LogDebug("Validating course for update with {CourseId}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating course with ID {CourseId}: {Errors}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }
            
            logger.LogInformation("Attempting to update mentor with ID {CourseId}", id);
            var courseToUpdate = await repository.GetByIdAsync(id);
            if (courseToUpdate == null)
            {
                logger.LogWarning("Update failed: Course with ID {CourseId} not found.", id);
                throw new NotFoundException(nameof(Course), id);
            }

            mapper.Map(dto, courseToUpdate);
            await repository.UpdateAsync(courseToUpdate);

            logger.LogInformation("Successfully updated course with ID {CourseId}", id);
            return mapper.Map<CourseDto>(courseToUpdate);
        }

        public async Task DeleteCourseAsync(int id)
        {
            logger.LogInformation("Attempting to delete course with ID {course}", id);
            var courseToDelete = await repository.GetByIdAsync(id);
            if (courseToDelete == null)
            {
                logger.LogWarning("Delete failed: Course with ID {CourseId} not found.", id);
                throw new NotFoundException(nameof(Course), id);
            }

            await repository.DeleteAsync(courseToDelete);
            logger.LogInformation("Successfully deleted course with ID {MentorId}", id);
        }
    }
}
