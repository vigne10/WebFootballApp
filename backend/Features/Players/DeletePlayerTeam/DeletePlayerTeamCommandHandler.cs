using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.DeletePlayerTeam;

public class DeletePlayerTeamCommandHandler : IRequestHandler<DeletePlayerTeamCommand, ErrorOr<Player>>
{
    private readonly ApplicationDbContext _context;

    public DeletePlayerTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Player>> Handle(DeletePlayerTeamCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Player
            .Include(p => p.Position)
            .Include(p => p.Team)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (player == null) return Error.NotFound("Player not found");

        player.Team = null;

        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }
}