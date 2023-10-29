using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Positions.UpdatePosition;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, ErrorOr<Position>>
{
    private readonly ApplicationDbContext _context;

    public UpdatePositionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Position>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Position.FindAsync(request.Id);

        if (position == null) return Error.NotFound("Position not found");

        if (!string.IsNullOrEmpty(request.Name)) position.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Abbreviation)) position.Abbreviation = request.Abbreviation;

        await _context.SaveChangesAsync(cancellationToken);

        return position;
    }
}