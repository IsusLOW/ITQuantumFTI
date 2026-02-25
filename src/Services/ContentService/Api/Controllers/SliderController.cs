using ContentService.Application.DTOs;
using ContentService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SliderController(ISliderAppService sliderAppService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<SliderDto>>> GetSlides()
        {
            var slides = await sliderAppService.GetSlidesAsync();
            return Ok(slides);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SliderDto>> CreateSlide([FromBody] CreateSlideDto createSlideDto)
        {
            var newSlide = await sliderAppService.CreateSlideAsync(createSlideDto);
            return Ok(newSlide);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SliderDto>> UpdateSlide(int id, [FromBody] UpdateSlideDto updateSlideDto)
        {
            var updatedSlide = await sliderAppService.UpdateSlideAsync(id, updateSlideDto);
            return Ok(updatedSlide);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            await sliderAppService.DeleteSlideAsync(id);
            return NoContent();
        }
    }
}
