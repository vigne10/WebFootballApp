using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.ReadPlayers;

public class ReadPlayersCommandHandler : IRequestHandler<ReadPlayersCommand, ErrorOr<List<Player>>>
{
    private readonly ApplicationDbContext _context;
    
    public ReadPlayersCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<List<Player>>> Handle(ReadPlayersCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.TeamId);
        
        if (team == null) return Error.NotFound("Team not found");

        var players = await _context.Player
            .Include(p => p.Position)
            .Where(p => p.Team == team)
            .OrderBy(p => p.Surname) // Sorting by name
            .ToListAsync(cancellationToken);

        return players;
    }
}