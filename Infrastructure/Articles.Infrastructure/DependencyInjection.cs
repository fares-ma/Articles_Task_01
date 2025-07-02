using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Articles.Domain;
using Articles.Infrastructure.Repositories;

namespace Articles.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IArticleRepository, ArticleRepository>();
            return services;
        }
    }
} 