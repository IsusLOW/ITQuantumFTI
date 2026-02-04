using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MentorService.Application.DTOs;
using MentorService.Application.Repositories;
using MentorService.Domain.Entities;
using Microsoft.Extensions.Logging;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace MentorService.Application.Services
{
    public class MentorAppService(
        IMentorRepository repository,
        IMapper mapper,
        ILogger<MentorAppService> logger,
        IValidator<CreateMentorDto> createValidator,
        IValidator<UpdateMentorDto> updateValidator) : IMentorAppService
    {
        public async Task<IReadOnlyList<MentorDto>> GetMentorsAsync()
        {
            logger.LogInformation("Fetching all mentors");
            var mentors = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<MentorDto>>(mentors);
        }

        public async Task<MentorDto> GetMentorByIdAsync(int id)
        {
            logger.LogInformation("Fetching mentor with ID {MentorId}", id);
            var mentor = await repository.GetByIdAsync(id);
            if (mentor == null)
            {
                logger.LogWarning("Mentor with ID {MentorId} not found.", id);
                throw new NotFoundException("Mentor", id);
            }
            return mapper.Map<MentorDto>(mentor);
        }

        public async Task<MentorDto> CreateMentorAsync(CreateMentorDto dto)
        {
            logger.LogDebug("Validating new mentor");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating mentor: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new mentor");
            var mentor = mapper.Map<Mentor>(dto);
            await repository.AddAsync(mentor);
            
            logger.LogInformation("Successfully created mentor with ID {MentorId}", mentor.Id);
            return mapper.Map<MentorDto>(mentor);
        }

        public async Task<MentorDto> UpdateMentorAsync(int id, UpdateMentorDto dto)
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
            return mapper.Map<MentorDto>(existingMentor);
        }

        public async Task DeleteMentorAsync(int id)
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
