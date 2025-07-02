using Articles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;
        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<IEnumerable<Article>> GetByTitleAsync(string title)
        {
            return await _context.Articles
                .Where(a => a.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Articles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
} 