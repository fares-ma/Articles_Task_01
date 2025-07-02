using Articles.Application.DTOs;
using Articles.Application.Interfaces;
using Articles.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Articles.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repo;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await _repo.GetByIdAsync(id);
            if (article == null)
            {
                return null;
            }
            var dto = _mapper.Map<ArticleDto>(article);
            return dto;
        }

        public async Task<IEnumerable<ArticleDto>> GetByTitleAsync(string title)
        {
            var articles = await _repo.GetByTitleAsync(title);
            var dtos = _mapper.Map<IEnumerable<ArticleDto>>(articles);
            return dtos;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var articles = await _repo.GetAllAsync(pageNumber, pageSize);
            var dtos = _mapper.Map<IEnumerable<ArticleDto>>(articles);
            return dtos;
        }
    }
} 