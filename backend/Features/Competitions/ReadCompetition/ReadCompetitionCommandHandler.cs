using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.ReadCompetition;

public class ReadCompetitionCommandHandler : IRequestHandler<ReadCompetitionCommand, ErrorOr<Competition>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadCompetitionCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Competition>> Handle(ReadCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = _context.Competition.SingleOrDefault(c => c.Id == request.Id);

        if (competition == null) return Error.NotFound("Competition not found");

        if (competition.Logo != null)
            competition.Logo =
                $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Competitions/{competition.Logo}";

        return competition;
    }
}