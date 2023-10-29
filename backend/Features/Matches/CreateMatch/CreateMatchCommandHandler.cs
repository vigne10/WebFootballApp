using System.Globalization;
using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.CreateMatch;

public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, ErrorOr<Match>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public CreateMatchCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Match>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);
        var homeTeam = await _context.Team.FindAsync(request.HomeTeamId);
        var awayTeam = await _context.Team.FindAsync(request.AwayTeamId);

        if (competitionSeason == null || homeTeam == null || awayTeam == null)
            return Error.NotFound("Competition season, Home Team, or Away Team not found");

        var match = new Match
        {
            CompetitionSeason = competitionSeason,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeTeamScore = request.HomeTeamScore,
            AwayTeamScore = request.AwayTeamScore
        };

        if (!string.IsNullOrEmpty(request.Schedule))
        {
            if (DateTime.TryParseExact(request.Schedule, "yyyy-MM-ddTHH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                match.Schedule = date;
            else
                return Error.Failure("Invalid date and time for the match");
        }

        _context.Match.Add(match);
        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(competitionSeason);

        return match;
    }
}