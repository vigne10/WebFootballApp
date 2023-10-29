namespace WebFootballApp.Entities;

public class History
{
    public int Id { get; set; }
    public User User { get; set; }
    public string? Content { get; set; }
    public DateTime Date { get; set; }
}