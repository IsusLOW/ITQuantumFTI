using SliderService.Application.Repositories;
using SliderService.Domain.Entities;

namespace SliderService.Infrastructure.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        public Task AddAsync(Slider slider)
        {
            // TODO: Implement data access logic here
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Slider slider)
        {
            // TODO: Implement data access logic here
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Slider>> GetAllAsync()
        {
            // TODO: Implement data access logic here
            return Task.FromResult(Enumerable.Empty<Slider>());
        }

        public Task<Slider?> GetByIdAsync(int id)
        {
            // TODO: Implement data access logic here
            return Task.FromResult<Slider?>(null);
        }

        public Task UpdateAsync(Slider slider)
        {
            // TODO: Implement data access logic here
            return Task.CompletedTask;
        }
    }
}
