using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Seasons.UpdateSeason;

public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand, ErrorOr<Season>>
{
    private readonly ApplicationDbContext _context;

    public UpdateSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Season>> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = await _context.Season.FindAsync(request.Id);

        if (season == null) return Error.NotFound("Season not found");

        season.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return season;
    }
}