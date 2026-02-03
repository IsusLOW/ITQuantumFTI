using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Common.Exceptions;
using SliderService.Application.DTOs;
using SliderService.Application.Repositories;
using SliderService.Application.Validators;
using SliderService.Domain.Entities;
using ValidationException = FluentValidation.ValidationException;

namespace SliderService.Application.Services
{
    public class SliderService(ISliderRepository _repository, IMapper _mapper, ILogger<SliderService> _logger) : ISliderService
    {
        public async Task<IReadOnlyList<SliderDto>> GetSlidesAsync()
        {
            _logger.LogInformation("Getting all slides");
            var sliders = await _repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<SliderDto>>(sliders);
        }

        public async Task<SliderDto> CreateSlideAsync(CreateSlideDto dto)
        {
            _logger.LogInformation("Creating a new slide.");
            
            var validator = new CreateSlideDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid) 
            {
                throw new ValidationException(validationResult.Errors);
            }

            var slider = _mapper.Map<Slider>(dto);

            await _repository.AddAsync(slider);
            
            _logger.LogInformation("Slide with id {Id} was created.", slider.Id);

            return _mapper.Map<SliderDto>(slider);
        }

        public async Task<SliderDto> UpdateSlideAsync(int id, UpdateSlideDto dto)
        {
            _logger.LogInformation("Updating slide with id: {id}", id);
            
            var validator = new UpdateSlideDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            var slider = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Slider", id);

            _mapper.Map(dto, slider);

            await _repository.UpdateAsync(slider);
            
            _logger.LogInformation("Slide with id {id} was updated.", id);

            return _mapper.Map<SliderDto>(slider);
        }

        public async Task DeleteSlideAsync(int id)
        {
            _logger.LogInformation("Deleting slide with id: {id}", id);
            var slider = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Slider", id);

            await _repository.DeleteAsync(slider);
            _logger.LogInformation("Slide with id {id} was deleted.", id);
        }
    }
}