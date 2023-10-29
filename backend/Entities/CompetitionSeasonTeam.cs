namespace WebFootballApp.Entities;

public class CompetitionSeasonTeam
{
    public int Id { get; set; }
    public CompetitionSeason CompetitionSeason { get; set; }
    public Team Team { get; set; }
}