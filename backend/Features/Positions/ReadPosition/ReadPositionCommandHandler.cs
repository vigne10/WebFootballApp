using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Positions.ReadPosition;

public class ReadPositionCommandHandler : IRequestHandler<ReadPositionCommand, ErrorOr<Position>>
{
    private readonly ApplicationDbContext _context;

    public ReadPositionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Position>> Handle(ReadPositionCommand request, CancellationToken cancellationToken)
    {
        var position = _context.Position.SingleOrDefault(p => p.Id == request.Id);

        if (position == null) return Error.NotFound("Position not found");

        return position;
    }
}