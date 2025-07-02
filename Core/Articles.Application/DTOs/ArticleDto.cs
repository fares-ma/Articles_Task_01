namespace Articles.Application.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
} 