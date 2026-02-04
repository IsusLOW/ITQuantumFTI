using Microsoft.AspNetCore.Mvc;
using MentorService.Application.DTOs;
using MentorService.Application.Services;

namespace MentorService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MentorController(IMentorAppService mentorAppService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MentorDto>>> GetMentors()
        {
            var mentors = await mentorAppService.GetMentorsAsync();
            return Ok(mentors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MentorDto>> GetMentor(int id)
        {
            var mentor = await mentorAppService.GetMentorByIdAsync(id);
            return Ok(mentor);
        }

        [HttpPost]
        public async Task<ActionResult<MentorDto>> CreateMentor([FromBody] CreateMentorDto createMentorDto)
        {
            var createdMentor = await mentorAppService.CreateMentorAsync(createMentorDto);
            return Ok(createdMentor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MentorDto>> UpdateMentor(int id, [FromBody] UpdateMentorDto updateMentorDto)
        {
            var updatedMentor = await mentorAppService.UpdateMentorAsync(id, updateMentorDto);
            return Ok(updatedMentor);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            await mentorAppService.DeleteMentorAsync(id);
            return NoContent();
        }
    }
}
