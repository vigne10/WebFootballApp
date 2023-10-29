using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Rankings.ReadRankingForCompetitionSeason;

public class
    ReadRankingForCompetitionSeasonCommandHandler : IRequestHandler<ReadRankingForCompetitionSeasonCommand,
        ErrorOr<List<Ranking>>>
{
    private readonly ApplicationDbContext _context;

    public ReadRankingForCompetitionSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Ranking>>> Handle(ReadRankingForCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);
        if (competitionSeason == null)
            return Error.NotFound("Competition season not found");

        var rankings = await _context.Ranking
            .Include(r => r.CompetitionSeason)
            .Include(r => r.CompetitionSeason.Competition)
            .Include(r => r.CompetitionSeason.Season)
            .Where(r => r.CompetitionSeason == competitionSeason)
            .Include(r => r.Team)
            .OrderByDescending(r => r.Points > 0)
            .ThenByDescending(r => r.Points)
            .ThenByDescending(r => r.GoalsDifference)
            .ThenBy(r => r.Team.Name)
            .ToListAsync(cancellationToken);

        return rankings;
    }
}