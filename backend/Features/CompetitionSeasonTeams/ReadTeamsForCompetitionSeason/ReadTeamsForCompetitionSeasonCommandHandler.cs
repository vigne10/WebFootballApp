using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasonTeams.ReadTeamsForCompetitionSeason;

public class ReadTeamsForCompetitionSeasonCommandHandler : IRequestHandler<ReadTeamsForCompetitionSeasonCommand,
    ErrorOr<List<CompetitionSeasonTeam>>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadTeamsForCompetitionSeasonCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<List<CompetitionSeasonTeam>>> Handle(ReadTeamsForCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeasonTeams = await _context.CompetitionSeasonTeam
            .Include(x => x.Team)
            .Include(x => x.CompetitionSeason)
            .Include(x => x.CompetitionSeason.Competition)
            .Include(x => x.CompetitionSeason.Season)
            .Where(x => x.CompetitionSeason.Id == request.CompetitionSeasonId)
            .OrderBy(x => x.Team.Name)
            .ToListAsync(cancellationToken);

        if (competitionSeasonTeams == null)
            return Error.NotFound("Teams for this competition season not found");

        foreach (var competitionSeasonTeam in competitionSeasonTeams)
            competitionSeasonTeam.Team.Logo = competitionSeasonTeam.Team.Logo != null
                ? $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Teams/{competitionSeasonTeam.Team.Logo}"
                : null;

        return competitionSeasonTeams;
    }
}