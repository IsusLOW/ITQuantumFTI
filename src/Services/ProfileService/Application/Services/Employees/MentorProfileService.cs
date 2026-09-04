using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ProfileService.Application.DTOs.Employees.Mentors;
using ProfileService.Application.Repositories.Employees;
using ProfileService.Domain.Employees;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;


namespace ProfileService.Application.Services.Employees
{
    public class MentorProfileService(
        IMentorProfileRepository repository,
        IMapper mapper,
        ILogger<MentorProfileService> logger,
        IValidator<CreateMentorProfileDto> createValidator,
        IValidator<UpdateMentorProfileDto> updateValidator)
        : IMentorProfileService
    {
        public async Task<IReadOnlyList<MentorProfileDto>> GetProfilesAsync()
        {
            logger.LogInformation("Fetching all mentors");
            var mentors = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<MentorProfileDto>>(mentors);
        }

        public async Task<MentorProfileDto> GetProfileByIdAsync(int id)
        {
            logger.LogInformation("Fetching mentor with ID {MentorId}", id);
            var mentor = await repository.GetByIdAsync(id);
            if (mentor == null)
            {
                logger.LogWarning("Mentor with ID {MentorId} not found.", id);
                throw new NotFoundException("Mentor", id);
            }
            return mapper.Map<MentorProfileDto>(mentor);
        }

        public async Task<MentorProfileDto> CreateProfileAsync(CreateMentorProfileDto dto)
        {
            logger.LogDebug("Validating new mentor");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating mentor: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new mentor");
            var mentor = mapper.Map<MentorProfile>(dto);
            await repository.AddAsync(mentor);
            
            logger.LogInformation("Successfully created mentor with ID {MentorId}", mentor.Id);
            return mapper.Map<MentorProfileDto>(mentor);
        }

        public async Task<MentorProfileDto> UpdateProfileAsync(int id, UpdateMentorProfileDto dto)
        {
            logger.LogDebug("Validating mentor for update with ID {MentorId}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating mentor with ID {MentorId}: {Errors}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Attempting to update mentor with ID {MentorId}", id);
            var existingMentor = await repository.GetByIdAsync(id);
            if (existingMentor == null)
            {
                logger.LogWarning("Update failed: Mentor with ID {MentorId} not found.", id);
                throw new NotFoundException("Mentor", id);
            }

            mapper.Map(dto, existingMentor);
            await repository.UpdateAsync(existingMentor);

            logger.LogInformation("Successfully updated mentor with ID {MentorId}", id);
            return mapper.Map<MentorProfileDto>(existingMentor);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Attempting to delete mentor with ID {MentorId}", id);
            var mentorToDelete = await repository.GetByIdAsync(id);
            if (mentorToDelete == null)
            {
                logger.LogWarning("Delete failed: Mentor with ID {MentorId} not found.", id);
                throw new NotFoundException("Mentor", id);
            }

            await repository.DeleteAsync(mentorToDelete);
            logger.LogInformation("Successfully deleted mentor with ID {MentorId}", id);
        }
    }
}