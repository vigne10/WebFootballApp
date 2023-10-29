using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Seasons.CreateSeason;

public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, ErrorOr<Season>>
{
    private readonly ApplicationDbContext _context;

    public CreateSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Season>> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = new Season
        {
            Name = request.Name
        };

        _context.Season.Add(season);
        await _context.SaveChangesAsync(cancellationToken);

        return season;
    }
}