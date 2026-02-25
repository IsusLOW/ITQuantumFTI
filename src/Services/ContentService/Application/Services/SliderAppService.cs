using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Application.Repositories;
using ContentService.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Common.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace ContentService.Application.Services
{
    public class SliderAppService(
        ISliderRepository repository,
        IMapper mapper,
        ILogger<SliderAppService> logger,
        IValidator<CreateSlideDto> createValidator,
        IValidator<UpdateSlideDto> updateValidator) : ISliderAppService
    {
        public async Task<IReadOnlyList<SliderDto>> GetSlidesAsync()
        {
            logger.LogInformation("Getting all slides");
            var sliders = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<SliderDto>>(sliders);
        }

        public async Task<SliderDto> CreateSlideAsync(CreateSlideDto dto)
        {
            logger.LogDebug("Validating new slide");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating slide: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new slide");
            var slider = mapper.Map<Slider>(dto);
            await repository.AddAsync(slider);

            logger.LogInformation("Successfully created slide with ID {SlideId}", slider.Id);
            return mapper.Map<SliderDto>(slider);
        }

        public async Task<SliderDto> UpdateSlideAsync(int id, UpdateSlideDto dto)
        {
            logger.LogDebug("Validating slide for update with ID {SlideId}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating slide with ID {SlideId}: {Errors}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Attempting to update slide with ID {SlideId}", id);
            var existingSlider = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Slider", id);

            mapper.Map(dto, existingSlider);
            await repository.UpdateAsync(existingSlider);

            logger.LogInformation("Successfully updated slide with ID {SlideId}", id);
            return mapper.Map<SliderDto>(existingSlider);
        }

        public async Task DeleteSlideAsync(int id)
        {
            logger.LogInformation("Attempting to delete slide with ID {SlideId}", id);
            var sliderToDelete = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Slider", id);

            await repository.DeleteAsync(sliderToDelete);
            logger.LogInformation("Successfully deleted slide with ID {SlideId}", id);
        }
    }
}
