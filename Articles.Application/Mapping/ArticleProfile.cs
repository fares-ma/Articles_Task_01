using AutoMapper;
using Articles.Domain;
using Articles.Application.DTOs;
using System.Linq;

namespace Articles.Application.Mapping
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.ToList()));
            CreateMap<ArticleDto, Article>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.ToList()));
        }
    }
} 