using Microsoft.EntityFrameworkCore;
using Articles.Domain;
using System.Text.Json;

namespace Articles.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title).IsRequired();
                entity.Property(a => a.Description).IsRequired();
                // تخزين Tags كسلسلة نصية مفصولة بفواصل
                entity.Property(a => a.Tags)
                    .HasConversion(
                        v => string.Join(",", v),
                        v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });
        }
    }
} 