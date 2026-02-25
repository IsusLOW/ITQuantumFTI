using ContentService.Application.DTOs;
using ContentService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NewsController(INewsAppService newsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<NewsDto>>> GetNews()
        {
            var news = await newsService.GetNewsAsync();
            return Ok(news);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<IReadOnlyList<NewsDto>>> GetNewsWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var news = await newsService.GetNewsWithPaginationAsync(pageNumber, pageSize);
            return Ok(news);
        }


        [HttpPost]
        public async Task<ActionResult<NewsDto>> CreateNews([FromBody] CreateNewsDto createNewsDto)
        {
            var createdNews = await newsService.CreateNewsAsync(createNewsDto);
            return Ok(createdNews);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NewsDto>> UpdateNews(int id, [FromBody] UpdateNewsDto updateNewsDto)
        {
            var updatedNews = await newsService.UpdateNewsAsync(id, updateNewsDto);
            return Ok(updatedNews);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
