using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasonTeams.AddTeamToCompetitionSeason;

public class
    AddTeamToCompetitionSeasonCommandHandler : IRequestHandler<AddTeamToCompetitionSeasonCommand,
        ErrorOr<CompetitionSeasonTeam>>
{
    private readonly ApplicationDbContext _context;

    public AddTeamToCompetitionSeasonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<CompetitionSeasonTeam>> Handle(AddTeamToCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);

        if (competitionSeason == null)
            return Error.NotFound("Competition season not found");

        var teamToAdd = await _context.Team.FindAsync(request.TeamId);

        if (teamToAdd == null)
            return Error.NotFound("Team not found");

        // Load the associated competition and season
        await _context.Entry(competitionSeason)
            .Reference(cs => cs.Competition)
            .LoadAsync(cancellationToken);

        await _context.Entry(competitionSeason)
            .Reference(cs => cs.Season)
            .LoadAsync();

        // Check if the team is already in the competition for the season
        var existingEntry = await _context.CompetitionSeasonTeam
            .SingleOrDefaultAsync(cst => cst.CompetitionSeason == competitionSeason && cst.Team == teamToAdd,
                cancellationToken);

        if (existingEntry != null)
            return Error.Conflict("Team already in competition");

        // Add the team to the competition for the season
        var competitionSeasonTeam = new CompetitionSeasonTeam
        {
            CompetitionSeason = competitionSeason,
            Team = teamToAdd
        };

        _context.CompetitionSeasonTeam.Add(competitionSeasonTeam);

        await _context.SaveChangesAsync(cancellationToken);

        return competitionSeasonTeam;
    }
}