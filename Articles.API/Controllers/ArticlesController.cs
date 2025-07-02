using Microsoft.AspNetCore.Mvc;
using Articles.Application.Interfaces;
using Articles.Application.DTOs;

namespace Articles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetById(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetByTitle([FromQuery] string title)
        {
            var articles = await _articleService.GetByTitleAsync(title);
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var articles = await _articleService.GetAllAsync(pageNumber, pageSize);
            return Ok(articles);
        }
    }
} 