using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.ReadTeamMatchesForSeason;

public class
    ReadTeamMatchesForSeasonCommandHandler : IRequestHandler<ReadTeamMatchesForSeasonCommand, ErrorOr<List<Match>>>
{
    private readonly ApplicationDbContext _context;

    public ReadTeamMatchesForSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Match>>> Handle(ReadTeamMatchesForSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var team = _context.Team.SingleOrDefault(t => t.Id == request.TeamId);
        if (team == null) return Error.NotFound("Team not found");

        var season = _context.Season.SingleOrDefault(s => s.Id == request.SeasonId);
        if (season == null) return Error.NotFound("Season not found");

        var matches = await _context.Match
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.CompetitionSeason)
            .Include(m => m.CompetitionSeason.Competition)
            .Where(m => m.HomeTeam == team || m.AwayTeam == team)
            .Where(m => m.CompetitionSeason.Season == season)
            .OrderBy(m => m.Schedule) // Sorting by date and time
            .ToListAsync(cancellationToken);

        return matches;
    }
}