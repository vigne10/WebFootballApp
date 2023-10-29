using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.UpdatePlayerTeam;

public class UpdatePlayerTeamCommandHandler : IRequestHandler<UpdatePlayerTeamCommand, ErrorOr<Player>>
{
    private readonly ApplicationDbContext _context;

    public UpdatePlayerTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Player>> Handle(UpdatePlayerTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.TeamId);
        if (team == null) return Error.NotFound("Team not found");

        var player = await _context.Player.Include(p => p.Position)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (player == null) return Error.NotFound("Player not found");

        player.Team = team;

        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }
}