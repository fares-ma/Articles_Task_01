using Articles.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var article = await _context.Articles.FindAsync(id);
            return article;
        }

        public async Task<IEnumerable<Article>> GetByTitleAsync(string title)
        {
            var articles = await _context.Articles
                .Where(a => a.Title.Contains(title))
                .ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(int pageNumber, int pageSize)
        {
            var articles = await _context.Articles
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return articles;
        }
    }
} 