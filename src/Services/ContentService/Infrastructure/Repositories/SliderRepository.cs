using ContentService.Application.Repositories;
using ContentService.Domain.Entities;

namespace ContentService.Infrastructure.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        private static readonly List<Slider> _sliders = new List<Slider>
        {
            new Slider { Id = 1, Title = "Slide 1", ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", CreatedAt = DateTime.UtcNow },
            new Slider { Id = 2, Title = "Slide 2", ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", CreatedAt = DateTime.UtcNow },
            new Slider { Id = 3, Title = "Slide 3", ImageUrl = "https://img.freepik.com/premium-vector/happy-new-year-2026-design-gold-blue-with-abstract-winter-pattern-with-christmas-tree-toy_1009332-514.jpg?semt=ais_hybrid&w=740&q=80", CreatedAt = DateTime.UtcNow }
        };
        private static int _nextId = 4;

        public Task AddAsync(Slider slider)
        {
            slider.Id = _nextId++;
            slider.CreatedAt = DateTime.UtcNow;
            _sliders.Add(slider);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Slider slider)
        {
            var sliderToRemove = _sliders.FirstOrDefault(s => s.Id == slider.Id);
            if (sliderToRemove != null)
            {
                _sliders.Remove(sliderToRemove);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Slider>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Slider>>(_sliders);
        }

        public Task<Slider?> GetByIdAsync(int id)
        {
            var slider = _sliders.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(slider);
        }

        public Task UpdateAsync(Slider slider)
        {
            var existingSlider = _sliders.FirstOrDefault(s => s.Id == slider.Id);
            if (existingSlider != null)
            {
                existingSlider.Title = slider.Title;
                existingSlider.ImageUrl = slider.ImageUrl;
            }
            return Task.CompletedTask;
        }
    }
}
