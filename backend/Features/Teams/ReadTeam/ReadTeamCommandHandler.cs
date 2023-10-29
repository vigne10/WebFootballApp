using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.ReadTeam;

public class ReadTeamCommandHandler : IRequestHandler<ReadTeamCommand, ErrorOr<Team>>
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ReadTeamCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Team>> Handle(ReadTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _context.Team.SingleOrDefault(t => t.Id == request.Id);

        if (team == null) return Error.NotFound("Team not found");
        
        if (team.Logo != null) team.Logo = $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Teams/{team.Logo}";
        
        return team;
    }
}