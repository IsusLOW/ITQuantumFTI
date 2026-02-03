using Microsoft.AspNetCore.Mvc;
using NewsService.Application.DTOs;
using NewsService.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsService.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class NewsController(INewsAppService newsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<NewsDto>>> GetNews()
        {
            var news = await newsService.GetNewsAsync();
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
