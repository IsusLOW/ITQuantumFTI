using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ProfileService.Application.DTOs.Clients;
using ProfileService.Application.Repositories.Clients;
using ProfileService.Domain.Clients;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace ProfileService.Application.Services.Clients
{
    public class StudentProfileService (
        IStudentProfileRepository repository,
        IMapper mapper,
        ILogger<StudentProfileDto> logger,
        IValidator<CreateStudentProfileDto> createValidator,
        IValidator<UpdateStudentProfileDto> updateValidator) 
        : IStudentProfileService
    {
        public async Task<IReadOnlyList<StudentProfileDto>> GetProfilesAsync()
        {
            logger.LogInformation("Fetching all students");
            var studentProfiles = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<StudentProfileDto>>(studentProfiles);
        }

        public async Task<StudentProfileDto> GetProfileByIdAsync(int id)
        {
            logger.LogInformation("Fetching student with ID {ProfileId}", id);
            var studentProfile = await repository.GetByIdAsync(id);
            return mapper.Map<StudentProfileDto>(studentProfile);
        }

        public async Task<StudentProfileDto> CreateProfileAsync(CreateStudentProfileDto dto)
        {
            logger.LogDebug("Validating new student profile");
            var validationResult = await createValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating student profile: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new student");
            var studentProfile = mapper.Map<ClientProfile>(dto);
            await repository.AddAsync(studentProfile);

            logger.LogInformation("Successfully created student with ID {Id}", studentProfile.Id);
            return mapper.Map<StudentProfileDto>(studentProfile);
        }

        public async Task<StudentProfileDto> UpdateProfileAsync(int id, UpdateStudentProfileDto dto)
        {
            logger.LogDebug("Validating student for update with ID {Id}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating student with ID {id} : {Err}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Attempting to update student with ID {Id}", id);
            var existingParent = await repository.GetByIdAsync(id);
            if(existingParent == null)
            {
                logger.LogWarning("Update failed: Student with ID {Id} not found", id);
                throw new NotFoundException("Student", id);
            }

            mapper.Map(dto, existingParent);
            await repository.UpdateAsync(existingParent);

            logger.LogInformation("Successfully updated student with ID {Id}", id);
            return mapper.Map<StudentProfileDto>(existingParent);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Attempting to delete student with ID {Id}", id);
            var studentToDelete = await repository.GetByIdAsync(id);
            if (studentToDelete == null)
            {
                logger.LogWarning("Delete failed: Student with ID {Id} not found.", id);
                throw new NotFoundException("Student", id);
            }

            await repository.DeleteAsync(studentToDelete);
            logger.LogInformation("Successfully deleted student with ID {Id}", id);
        }
    }
}