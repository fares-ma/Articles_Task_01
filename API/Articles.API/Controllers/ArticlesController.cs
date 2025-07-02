using Microsoft.AspNetCore.Mvc;
using Articles.Application.Interfaces;
using Articles.Application.DTOs;

namespace Articles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;
        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest();
            }
            var articles = await _service.GetByTitleAsync(title);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAllArticles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var articles = await _service.GetAllAsync(pageNumber, pageSize);

            if (articles == null || !articles.Any())
            {
                return NotFound();
            }

            return Ok(articles);
        }
    }
} 