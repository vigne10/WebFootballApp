using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasons.CreateSeasonForCompetition;

public class
    CreateSeasonForCompetitionCommandHandler : IRequestHandler<CreateSeasonForCompetitionCommand,
        ErrorOr<CompetitionSeason>>
{
    private readonly ApplicationDbContext _context;

    public CreateSeasonForCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<CompetitionSeason>> Handle(CreateSeasonForCompetitionCommand request,
        CancellationToken cancellationToken)
    {
        // Get competition and season
        var competition = _context.Competition.Find(request.CompetitionId);
        if (competition == null)
            return Error.NotFound("Competition not found");

        var season = _context.Season.Find(request.SeasonId);
        if (season == null)
            return Error.NotFound("Season not found");

        // Check if season already exists for competition
        var competitionSeason = await _context.CompetitionSeason
            .Where(cs => cs.Competition == competition && cs.Season == season)
            .FirstOrDefaultAsync(cancellationToken);

        if (competitionSeason != null)
            return Error.Conflict("Season already exists for competition");

        // Create competition season
        competitionSeason = new CompetitionSeason
        {
            Competition = competition,
            Season = season
        };

        _context.CompetitionSeason.Add(competitionSeason);
        await _context.SaveChangesAsync(cancellationToken);

        return competitionSeason;
    }
}