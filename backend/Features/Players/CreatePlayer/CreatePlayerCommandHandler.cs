using System.Globalization;
using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.CreatePlayer;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, ErrorOr<Player>>
{
    private readonly ApplicationDbContext _context;

    public CreatePlayerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Player>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player();

        if (!string.IsNullOrEmpty(request.Birthday))
        {
            if (DateTime.TryParseExact(request.Birthday, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
                player.Birthday = birthday;
            else
                return Error.Failure("Invalid Birthday");
        }

        player.Name = request.Name;
        player.Surname = request.Surname;

        _context.Player.Add(player);
        await _context.SaveChangesAsync(cancellationToken);

        return player;
    }
}