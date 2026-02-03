 
using Microsoft.AspNetCore.Mvc;
using SliderService.Application.DTOs;
using SliderService.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Asp.Versioning;

namespace SliderService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class SliderController(ISliderService _sliderService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SliderDto>>> GetSlides()
        {
            var slides = await _sliderService.GetSlidesAsync();
            return Ok(slides);
        }

        [HttpPost]
        public async Task<ActionResult<SliderDto>> CreateSlide([FromBody] CreateSlideDto createSlideDto)
        {
            var newSlide = await _sliderService.CreateSlideAsync(createSlideDto);
            return Ok(newSlide);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SliderDto>> UpdateSlide(int id, [FromBody] UpdateSlideDto updateSlideDto)
        {
            var updatedSlide = await _sliderService.UpdateSlideAsync(id, updateSlideDto);
            return Ok(updatedSlide);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            await _sliderService.DeleteSlideAsync(id);
            return NoContent();
        }
    }
}
