using ContentService.Domain.Entities;

namespace ContentService.Application.Repositories
{
    public interface ISliderRepository
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider?> GetByIdAsync(int id);
        Task AddAsync(Slider slider);
        Task UpdateAsync(Slider slider);
        Task DeleteAsync(Slider slider);
    }
}
