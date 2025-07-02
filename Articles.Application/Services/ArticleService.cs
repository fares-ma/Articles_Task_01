using Articles.Application.DTOs;
using Articles.Application.Interfaces;
using Articles.Domain;
using AutoMapper;

namespace Articles.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            return article == null ? null : _mapper.Map<ArticleDto>(article);
        }

        public async Task<IEnumerable<ArticleDto>> GetByTitleAsync(string title)
        {
            var articles = await _repository.GetByTitleAsync(title);
            return _mapper.Map<IEnumerable<ArticleDto>>(articles);
        }

        public async Task<IEnumerable<ArticleDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var articles = await _repository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ArticleDto>>(articles);
        }
    }
} 