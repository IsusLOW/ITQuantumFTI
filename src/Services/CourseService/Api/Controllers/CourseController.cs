using Microsoft.AspNetCore.Mvc;
using CourseService.Application.Services;
using CourseService.Application.DTOs;


namespace CourseService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CourseController(ICourseAppService courseAppService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetCourses()
        {
            var courses = await courseAppService.GetCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await courseAppService.GetCourseByIdAsync(id);
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            var createdCourse = await courseAppService.CreateCourseAsync(createCourseDto);
            return Ok(createdCourse);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            var updatedCourse = await courseAppService.UpdateCourseAsync(id, updateCourseDto);
            return Ok(updatedCourse);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await courseAppService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}