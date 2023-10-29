using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.ReadMatchesForCompetitionSeason;

public class
    ReadMatchesForCompetitionSeasonCommandHandler : IRequestHandler<ReadMatchesForCompetitionSeasonCommand,
        ErrorOr<List<Match>>>
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ReadMatchesForCompetitionSeasonCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<List<Match>>> Handle(ReadMatchesForCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);

        if (competitionSeason == null) return Error.NotFound("Competition season not found");

        var matches = await _context.Match
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.CompetitionSeason.Competition)
            .Include(m => m.CompetitionSeason.Season)
            .Where(m => m.CompetitionSeason == competitionSeason)
            .OrderBy(m => m.Schedule) // Sorting by date and time
            .ToListAsync(cancellationToken);

        return matches;
    }
}