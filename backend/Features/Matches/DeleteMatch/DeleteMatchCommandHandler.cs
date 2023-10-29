using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.DeleteMatch;

public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public DeleteMatchCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _context.Match.Include(m => m.CompetitionSeason)
            .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (match == null) return Error.NotFound("Match not found");

        _context.Match.Remove(match);
        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(match.CompetitionSeason);

        return Unit.Value;
    }
}