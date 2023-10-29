using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Matches.ReadMatch;

public class ReadMatchCommandHandler : IRequestHandler<ReadMatchCommand, ErrorOr<Match>>
{
    private readonly ApplicationDbContext _context;

    public ReadMatchCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Match>> Handle(ReadMatchCommand request, CancellationToken cancellationToken)
    {
        var match = _context.Match
            .Include(m => m.CompetitionSeason)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .SingleOrDefault(m => m.Id == request.Id);

        if (match == null) return Error.NotFound("Match not found");

        return match;
    }
}