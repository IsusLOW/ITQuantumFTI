using ContentService.Application.DTOs;
using ContentService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GalleryController(IGalleryAppService galleryAppService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GalleryImageDto>>> GetImages()
        {
            var images = await galleryAppService.GetAllAsync();
            return Ok(images);
        }
    }
}
