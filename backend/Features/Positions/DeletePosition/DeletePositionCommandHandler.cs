using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Positions.DeletePosition;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeletePositionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Position.FindAsync(request.Id);

        if (position == null) return Error.NotFound("Position not found");

        _context.Position.Remove(position);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}