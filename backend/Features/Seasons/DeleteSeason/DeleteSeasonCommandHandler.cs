using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Seasons.DeleteSeason;

public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = await _context.Season.FindAsync(request.Id);

        if (season == null) return Error.NotFound("Season not found");

        _context.Season.Remove(season);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}