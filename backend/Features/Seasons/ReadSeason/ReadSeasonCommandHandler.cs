using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Seasons.ReadSeason;

public class ReadSeasonCommandHandler : IRequestHandler<ReadSeasonCommand, ErrorOr<Season>>
{
    private readonly ApplicationDbContext _context;

    public ReadSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Season>> Handle(ReadSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = _context.Season.SingleOrDefault(s => s.Id == request.Id);

        if (season == null) return Error.NotFound("Season not found");

        return season;
    }
}