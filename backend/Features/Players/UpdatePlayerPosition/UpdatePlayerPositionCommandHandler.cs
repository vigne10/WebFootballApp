using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.UpdatePlayerPosition;

public class UpdatePlayerPositionCommandHandler : IRequestHandler<UpdatePlayerPositionCommand, ErrorOr<Player>>
{
    private readonly ApplicationDbContext _context;

    public UpdatePlayerPositionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Player>> Handle(UpdatePlayerPositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Position.FindAsync(request.PositionId);
        if (position == null) return Error.NotFound("Position not found");

        var player = await _context.Player.Include(p => p.Team)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (player == null) return Error.NotFound("Player not found");

        player.Position = position;

        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }
}