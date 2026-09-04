using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ProfileService.Application.DTOs.Clients;
using ProfileService.Application.DTOs.Employees.Mentors;
using ProfileService.Application.Repositories.Clients;
using ProfileService.Domain.Clients;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;


namespace ProfileService.Application.Services.Clients
{
    public class ParentProfileService(
        IParentProfileRepository repository,
        IMapper mapper,
        ILogger<ParentProfileService> logger,
        IValidator<CreateParentProfileDto> createValidator,
        IValidator<UpdateParentProfileDto> updateValidator) 
        : IParentProfileService
    {
        public async Task<IReadOnlyList<ParentProfileDto>> GetProfilesAsync()
        {
            logger.LogInformation("Fetching all parents");
            var parentProfiles = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<ParentProfileDto>>(parentProfiles);
        }

        public async Task<ParentProfileDto> GetProfileByIdAsync(int id)
        {
            logger.LogInformation("Fetching parent with ID {ProfileId}", id);
            var parentProfile = await repository.GetByIdAsync(id);
            return mapper.Map<ParentProfileDto>(parentProfile);
        }

        public async Task<ParentProfileDto> CreateProfileAsync(CreateParentProfileDto dto)
        {
            logger.LogDebug("Validating new parent profile");
            var validationResult = await createValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating parent profile: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new parent");
            var parentProfile = mapper.Map<ClientProfile>(dto);
            await repository.AddAsync(parentProfile);

            logger.LogInformation("Successfully created parent with ID {ParentId}", parentProfile.Id);
            return mapper.Map<ParentProfileDto>(parentProfile);
        }

        public async Task<ParentProfileDto> UpdateProfileAsync(int id, UpdateParentProfileDto dto)
        {
            logger.LogDebug("Validating parent for update with ID {Id}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating parent with ID {id} : {Err}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Attempting to update parent with ID {Id}", id);
            var existingParent = await repository.GetByIdAsync(id);
            if(existingParent == null)
            {
                logger.LogWarning("Update failed: Parent with ID {Id} not found", id);
                throw new NotFoundException("Parent", id);
            }

            mapper.Map(dto, existingParent);
            await repository.UpdateAsync(existingParent);

            logger.LogInformation("Successfully updated parnet with ID {Id}", id);
            return mapper.Map<ParentProfileDto>(existingParent);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Attempting to delete parent with ID {Id}", id);
            var parentToDelete = await repository.GetByIdAsync(id);
            if (parentToDelete == null)
            {
                logger.LogWarning("Delete failed: Parent with ID {Id} not found.", id);
                throw new NotFoundException("Parent", id);
            }

            await repository.DeleteAsync(parentToDelete);
            logger.LogInformation("Successfully deleted parent with ID {Id}", id);
        }
    }
}