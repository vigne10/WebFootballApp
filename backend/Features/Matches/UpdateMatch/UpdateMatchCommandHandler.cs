using System.Globalization;
using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.UpdateMatch;

public class UpdateMatchCommandHandler : IRequestHandler<UpdateMatchCommand, ErrorOr<Match>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public UpdateMatchCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Match>> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _context.Match.FindAsync(request.Id);
        if (match == null) return Error.NotFound("Match not found");

        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);
        var homeTeam = await _context.Team.FindAsync(request.HomeTeamId);
        var awayTeam = await _context.Team.FindAsync(request.AwayTeamId);

        if (competitionSeason == null || homeTeam == null || awayTeam == null)
            return Error.NotFound("Competition season, Home Team, or Away Team not found");

        match.CompetitionSeason = competitionSeason;
        match.HomeTeam = homeTeam;
        match.AwayTeam = awayTeam;
        match.HomeTeamScore = request.HomeTeamScore;
        match.AwayTeamScore = request.AwayTeamScore;

        if (!string.IsNullOrEmpty(request.Schedule))
        {
            if (DateTime.TryParseExact(request.Schedule, "yyyy-MM-ddTHH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                match.Schedule = date;
            else
                return Error.Failure("Invalid date and time for the match");
        }

        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(competitionSeason);

        return match;
    }
}