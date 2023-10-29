using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Positions.ReadPositions;

public class ReadPositionsCommandHandler : IRequestHandler<ReadPositionsCommand, ErrorOr<List<Position>>>
{
    private readonly ApplicationDbContext _context;

    public ReadPositionsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Position>>> Handle(ReadPositionsCommand request, CancellationToken cancellationToken)
    {
        var positions = _context.Position.OrderBy(p => p.Name).ToListAsync(cancellationToken);

        if (positions == null) return Error.NotFound("Positions not found");

        return await positions;
    }
}