using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasons.ReadCompetitionSeason;

public class
    ReadCompetitionSeasonCommandHandler : IRequestHandler<ReadCompetitionSeasonCommand, ErrorOr<CompetitionSeason>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadCompetitionSeasonCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<CompetitionSeason>> Handle(ReadCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.Include(cs => cs.Competition).Include(cs => cs.Season)
            .SingleOrDefaultAsync(cs => cs.Id == request.Id);

        if (competitionSeason == null) return Error.NotFound("CompetitionSeason not found");

        if (competitionSeason.Competition.Logo != null)
            competitionSeason.Competition.Logo =
                $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Competitions/{competitionSeason.Competition.Logo}";

        return competitionSeason;
    }
}