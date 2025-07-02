using Articles.Application.Interfaces;
using Articles.Application.Services;
using Articles.Application.Mapping;
using Articles.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=ArticlesDb;Trusted_Connection=True;";
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddAutoMapper(typeof(ArticleProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
