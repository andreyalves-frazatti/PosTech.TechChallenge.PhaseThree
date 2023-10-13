namespace TechChallenge.Domain.Entities;

public class News
{
    public News(string title, string content, DateTime date, string author)
    {
        Id = Guid.NewGuid();
        Title = title;
        Content = content;
        Date = date;
        Author = author;
    }

    public Guid Id { get; init; }
    
    public string Title { get; init; }

    public string Content { get; init; }

    public DateTime Date { get; init; }

    public string Author { get; init; }
}