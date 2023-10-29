using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Positions.CreatePosition;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, ErrorOr<Position>>
{
    private readonly ApplicationDbContext _context;
    
    public CreatePositionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<Position>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = new Position
        {
            Name = request.Name,
            Abbreviation = request.Abbreviation
        };
        
        _context.Position.Add(position);
        await _context.SaveChangesAsync(cancellationToken);

        return position;
    }
}