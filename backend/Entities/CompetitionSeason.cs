namespace WebFootballApp.Entities;

public class CompetitionSeason
{
    public int Id { get; set; }
    public Competition Competition { get; set; }
    public Season Season { get; set; }
}