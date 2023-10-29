using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.DeletePlayer;

public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeletePlayerCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Player.FindAsync(request.Id);

        if (player == null) return Error.NotFound("Player not found");

        _context.Player.Remove(player);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}