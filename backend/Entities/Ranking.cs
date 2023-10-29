namespace WebFootballApp.Entities;

public class Ranking
{
    public int Id { get; set; }
    public CompetitionSeason CompetitionSeason { get; set; }
    public Team Team { get; set; }
    public int? Played { get; set; }
    public int? Wins { get; set; }
    public int? Draws { get; set; }
    public int? Losses { get; set; }
    public int? GoalsFor { get; set; }
    public int? GoalsAgainst { get; set; }
    public int? GoalsDifference { get; set; }
    public int? Points { get; set; }
}