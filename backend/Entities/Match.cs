namespace WebFootballApp.Entities;

public class Match
{
    public int Id { get; set; }
    public CompetitionSeason CompetitionSeason { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public DateTime? Schedule { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
}