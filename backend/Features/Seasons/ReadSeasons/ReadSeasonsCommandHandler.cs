using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Seasons.ReadSeasons;

public class ReadSeasonsCommandHandler : IRequestHandler<ReadSeasonsCommand, ErrorOr<List<Season>>>
{
    private readonly ApplicationDbContext _context;

    public ReadSeasonsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Season>>> Handle(ReadSeasonsCommand request, CancellationToken cancellationToken)
    {
        var seasons = await _context.Season.OrderByDescending(s => s.Name).ToListAsync(cancellationToken);

        if (seasons == null) return Error.NotFound("Seasons not found");

        return seasons;
    }
}