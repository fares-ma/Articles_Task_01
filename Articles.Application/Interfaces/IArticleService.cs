using Articles.Application.DTOs;

namespace Articles.Application.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto> GetByIdAsync(int id);
        Task<IEnumerable<ArticleDto>> GetByTitleAsync(string title);
        Task<IEnumerable<ArticleDto>> GetAllAsync(int pageNumber, int pageSize);
    }
} 