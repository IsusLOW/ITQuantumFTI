using CourseService.Application.DTOs;

namespace CourseService.Application.Services
{
    public interface ICourseAppService
    {
        Task<IReadOnlyList<CourseDto>> GetCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto dto);
        Task DeleteCourseAsync(int id);
    }
}
