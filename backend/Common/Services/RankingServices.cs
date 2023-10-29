using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Common.Services;

public class RankingServices
{
    private readonly ApplicationDbContext _context;

    public RankingServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public int GetPoints(int? wins, int? draws)
    {
        var points = 0;
        if (wins != null) points = 3 * wins.Value;
        if (draws != null) points += draws.Value;

        return points;
    }

    public async Task UpdateRankingsAsync(CompetitionSeason competitionSeason)
    {
        var competitionSeasonTeams = await _context.CompetitionSeasonTeam
            .Where(cst => cst.CompetitionSeason == competitionSeason)
            .Select(tcs => tcs.Team)
            .ToListAsync();

        var matches = await _context.Match
            .Where(m => m.CompetitionSeason == competitionSeason)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();

        var teams = await _context.Team.ToListAsync();

        foreach (var team in teams)
        {
            if (!competitionSeasonTeams.Contains(team))
                continue;

            var teamRanking = await _context.Ranking
                .Where(r => r.CompetitionSeason == competitionSeason && r.Team == team)
                .FirstOrDefaultAsync();

            if (teamRanking == null)
            {
                teamRanking = new Ranking
                {
                    CompetitionSeason = competitionSeason,
                    Team = team,
                    Points = 0,
                    Played = 0,
                    Wins = 0,
                    Draws = 0,
                    Losses = 0,
                    GoalsFor = 0,
                    GoalsAgainst = 0,
                    GoalsDifference = 0
                };

                _context.Ranking.Add(teamRanking);
            }

            // Calculate team's statistics based on match results
            var homeMatches =
                matches.Where(m => m.HomeTeam == team && m.HomeTeamScore != null && m.AwayTeamScore != null);
            var awayMatches =
                matches.Where(m => m.AwayTeam == team && m.HomeTeamScore != null && m.AwayTeamScore != null);

            teamRanking.Wins = homeMatches.Count(m => m.HomeTeamScore > m.AwayTeamScore)
                               + awayMatches.Count(m => m.AwayTeamScore > m.HomeTeamScore);

            teamRanking.Draws = homeMatches.Count(m => m.HomeTeamScore == m.AwayTeamScore)
                                + awayMatches.Count(m => m.AwayTeamScore == m.HomeTeamScore);

            teamRanking.Losses = homeMatches.Count(m => m.HomeTeamScore < m.AwayTeamScore)
                                 + awayMatches.Count(m => m.AwayTeamScore < m.HomeTeamScore);

            teamRanking.Played = teamRanking.Wins + teamRanking.Draws + teamRanking.Losses;

            teamRanking.Points = GetPoints(teamRanking.Wins, teamRanking.Draws);

            teamRanking.GoalsFor = homeMatches.Sum(m => m.HomeTeamScore) + awayMatches.Sum(m => m.AwayTeamScore);
            teamRanking.GoalsAgainst = homeMatches.Sum(m => m.AwayTeamScore) + awayMatches.Sum(m => m.HomeTeamScore);

            teamRanking.GoalsDifference = teamRanking.GoalsFor - teamRanking.GoalsAgainst;

            _context.Entry(teamRanking).State = teamRanking.Id == 0 ? EntityState.Added : EntityState.Modified;
        }

        await _context.SaveChangesAsync();
    }
}