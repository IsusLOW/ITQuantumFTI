using AutoMapper;
using ContentService.Application.DTOs;
using ContentService.Application.Repositories;
using ContentService.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared.Common.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace ContentService.Application.Services
{
    public class NewsAppService(
        INewsRepository repository,
        IMapper mapper,
        ILogger<NewsAppService> logger,
        IValidator<CreateNewsDto> createValidator,
        IValidator<UpdateNewsDto> updateValidator) : INewsAppService
    {
        public async Task<IReadOnlyList<NewsDto>> GetNewsAsync()
        {
            logger.LogInformation("Fetching all news");
            var news = await repository.GetAllAsync();
            return mapper.Map<IReadOnlyList<NewsDto>>(news);
        }

        public async Task<IReadOnlyList<NewsDto>> GetNewsWithPaginationAsync(int pageNumber, int pageSize)
        {
            logger.LogInformation("Fetching pagination news");
            var news = await repository.GetWithPaginationAsync(pageNumber, pageSize);
            return mapper.Map<IReadOnlyList<NewsDto>>(news);
        }

        public async Task<NewsDto> CreateNewsAsync(CreateNewsDto dto)
        {
            logger.LogDebug("Validating new news item");
            var validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for creating news: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Creating a new news item");
            var news = mapper.Map<News>(dto);
            await repository.AddAsync(news);

            logger.LogInformation("Successfully created news item with ID {NewsId}", news.Id);
            return mapper.Map<NewsDto>(news);
        }

        public async Task<NewsDto> UpdateNewsAsync(int id, UpdateNewsDto dto)
        {
            logger.LogDebug("Validating news item for update with ID {NewsId}", id);
            var validationResult = await updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogError("Validation failed for updating news with ID {NewsId}: {Errors}", id, validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            logger.LogInformation("Attempting to update news item with ID {NewsId}", id);
            var existingNews = await repository.GetByIdAsync(id);
            if (existingNews == null)
            {
                logger.LogWarning("Update failed: News item with ID {NewsId} not found.", id);
                throw new NotFoundException("News", id);
            }

            mapper.Map(dto, existingNews);
            await repository.UpdateAsync(existingNews);

            logger.LogInformation("Successfully updated news item with ID {NewsId}", id);
            return mapper.Map<NewsDto>(existingNews);
        }

        public async Task DeleteNewsAsync(int id)
        {
            logger.LogInformation("Attempting to delete news item with ID {NewsId}", id);
            var newsToDelete = await repository.GetByIdAsync(id);
            if (newsToDelete == null)
            {
                logger.LogWarning("Delete failed: News item with ID {NewsId} not found.", id);
                throw new NotFoundException("News", id);
            }

            await repository.DeleteAsync(newsToDelete);
            logger.LogInformation("Successfully deleted news item with ID {NewsId}", id);
        }
    }
}
