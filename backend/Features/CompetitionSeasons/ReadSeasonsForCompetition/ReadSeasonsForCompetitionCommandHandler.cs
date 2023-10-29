using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasons.ReadSeasonsForCompetition;

public class
    ReadSeasonsForCompetitionCommandHandler : IRequestHandler<ReadSeasonsForCompetitionCommand, ErrorOr<List<CompetitionSeason>>>
{
    private readonly ApplicationDbContext _context;

    public ReadSeasonsForCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<CompetitionSeason>>> Handle(ReadSeasonsForCompetitionCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeasons = await _context.CompetitionSeason
            .Include(cs => cs.Season)
            .Include(cs => cs.Competition)
            .Where(cs => cs.Competition.Id == request.CompetitionId)
            .OrderByDescending(cs => cs.Season.Name)
            .ToListAsync(cancellationToken);

        if (competitionSeasons == null)
            return Error.NotFound("Competition seasons not found");

        return competitionSeasons;
    }
}