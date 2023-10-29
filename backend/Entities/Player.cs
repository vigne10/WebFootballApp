namespace WebFootballApp.Entities;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Picture { get; set; }
    public Position? Position { get; set; }
    public Team? Team { get; set; }
}