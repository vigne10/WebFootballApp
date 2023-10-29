using System.Globalization;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.UpdatePlayer;

public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, ErrorOr<Player>>
{
    private readonly ApplicationDbContext _context;

    public UpdatePlayerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Player>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Player
            .Include(p => p.Position)
            .Include(p => p.Team)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (player == null) return Error.NotFound("Player not found");

        if (!string.IsNullOrEmpty(request.Name)) player.Name = request.Name;
        if (!string.IsNullOrEmpty(request.Surname)) player.Surname = request.Surname;

        if (request.Birthday != null && DateTime.TryParseExact(request.Birthday, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
            player.Birthday = birthday;
        else
            player.Birthday = null; // Put null if birthday is empty

        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }
}