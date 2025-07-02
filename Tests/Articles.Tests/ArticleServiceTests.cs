using Xunit;
using Moq;
using System.Threading.Tasks;
using Articles.Application.Interfaces;
using Articles.Application.DTOs;
using Articles.Domain;
using System.Collections.Generic;
using AutoMapper;
using Articles.Application.Services;
using Articles.Application.Mapping;
using System.Linq;

namespace Articles.Tests
{
    public class ArticleServiceTests
    {
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;

        public ArticleServiceTests()
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ArticleProfile>());
            _mapper = config.CreateMapper();
            _articleService = new ArticleService(_articleRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsArticleDto_WhenArticleExists()
        {
            // Arrange
            var article = new Article { Id = 1, Title = "Test", Description = "Desc", Tags = new List<string> { "tag1" } };
            _articleRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(article);

            // Act
            var result = await _articleService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(article.Id, result.Id);
            Assert.Equal(article.Title, result.Title);
            Assert.Equal(article.Description, result.Description);
            Assert.Equal(article.Tags, result.Tags);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenArticleDoesNotExist()
        {
            _articleRepositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Article)null);
            var result = await _articleService.GetByIdAsync(99);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByTitleAsync_ReturnsMatchingArticles()
        {
            var articles = new List<Article>
            {
                new Article { Id = 1, Title = "C# Basics", Description = "Desc1", Tags = new List<string>{"tech"} },
                new Article { Id = 2, Title = "C# Advanced", Description = "Desc2", Tags = new List<string>{"tech"} }
            };
            _articleRepositoryMock.Setup(r => r.GetByTitleAsync("C#")).ReturnsAsync(articles);
            var result = await _articleService.GetByTitleAsync("C#");
            Assert.Equal(2, result.Count());
            Assert.All(result, a => Assert.Contains("C#", a.Title));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsPaginatedArticles()
        {
            var articles = new List<Article>
            {
                new Article { Id = 1, Title = "A", Description = "Desc1", Tags = new List<string>{"t1"} },
                new Article { Id = 2, Title = "B", Description = "Desc2", Tags = new List<string>{"t2"} }
            };
            _articleRepositoryMock.Setup(r => r.GetAllAsync(1, 2)).ReturnsAsync(articles);
            var result = await _articleService.GetAllAsync(1, 2);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByTitleAsync_ReturnsEmpty_WhenNoMatch()
        {
            _articleRepositoryMock.Setup(r => r.GetByTitleAsync("notfound")).ReturnsAsync(new List<Article>());
            var result = await _articleService.GetByTitleAsync("notfound");
            Assert.Empty(result);
        }
    }
} 