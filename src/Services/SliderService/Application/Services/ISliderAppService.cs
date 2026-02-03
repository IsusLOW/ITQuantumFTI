using SliderService.Application.DTOs;

namespace SliderService.Application.Services
{
    public interface ISliderAppService
    {
        Task<IReadOnlyList<SliderDto>> GetSlidesAsync();
        Task<SliderDto> CreateSlideAsync(CreateSlideDto dto);
        Task<SliderDto> UpdateSlideAsync(int id, UpdateSlideDto dto);
        Task DeleteSlideAsync(int id);
    }
}