using System.Collections.Generic;
using System.Threading.Tasks;

namespace Articles.Domain
{
    public interface IArticleRepository
    {
        Task<Article> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetByTitleAsync(string title);
        Task<IEnumerable<Article>> GetAllAsync(int pageNumber, int pageSize);
    }
} 