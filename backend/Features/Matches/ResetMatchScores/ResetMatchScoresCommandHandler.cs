using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Common.Services;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.ResetMatchScores;

public class ResetMatchScoresCommandHandler : IRequestHandler<ResetMatchScoresCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public ResetMatchScoresCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Unit>> Handle(ResetMatchScoresCommand request, CancellationToken cancellationToken)
    {
        var match = await _context.Match.Include(m => m.CompetitionSeason)
            .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);

        if (match == null) return Error.NotFound("Match not found");

        match.HomeTeamScore = null;
        match.AwayTeamScore = null;

        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(match.CompetitionSeason);

        return Unit.Value;
    }
}