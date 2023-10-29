namespace WebFootballApp.Entities;

public class Reward
{
    public int Id { get; set; }
    public Team Team { get; set; }
    public CompetitionSeason CompetitionSeason { get; set; }
}