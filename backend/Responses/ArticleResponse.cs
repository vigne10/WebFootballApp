namespace WebFootballApp.Responses;

public class ArticleResponse
{
    public int Id { get; set; }

    public UserResponse User { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string? Image { get; set; }

    public DateTime Date { get; set; }

}