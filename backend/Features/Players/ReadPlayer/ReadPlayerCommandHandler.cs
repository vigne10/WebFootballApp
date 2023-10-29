using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.ReadPlayer;

public class ReadPlayerCommandHandler : IRequestHandler<ReadPlayerCommand, ErrorOr<Player>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadPlayerCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Player>> Handle(ReadPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = _context.Player
            .Include(p => p.Position)
            .Include(p => p.Team)
            .SingleOrDefault(p => p.Id == request.Id);

        if (player == null) return Error.NotFound("Player not found");

        if (player.Picture != null)
            player.Picture = $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Players/{player.Picture}";

        return player;
    }
}