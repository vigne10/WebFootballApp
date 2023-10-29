using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Rankings.GenerateRanking;

public class GenerateRankingCommandHandler : IRequestHandler<GenerateRankingCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public GenerateRankingCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Unit>> Handle(GenerateRankingCommand request, CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);
        if (competitionSeason == null) return Error.NotFound("Competition season not found");

        var rankings = await _context.Ranking
            .Include(r => r.CompetitionSeason)
            .Where(r => r.CompetitionSeason == competitionSeason)
            .ToListAsync(cancellationToken);

        foreach (var ranking in rankings) _context.Ranking.Remove(ranking);

        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(competitionSeason);

        return Unit.Value;
    }
}