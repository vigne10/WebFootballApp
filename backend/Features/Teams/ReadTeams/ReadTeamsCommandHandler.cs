using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.ReadTeams;

public class ReadTeamsCommandHandler : IRequestHandler<ReadTeamsCommand, ErrorOr<List<Team>>>
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ReadTeamsCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<List<Team>>> Handle(ReadTeamsCommand request, CancellationToken cancellationToken)
    {
        var teams = await _context.Team.OrderBy(t => t.Name).ToListAsync(cancellationToken);

        if (teams == null) return Error.NotFound("Teams not found");

        foreach (var team in teams)
            team.Logo = team.Logo != null ? $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Teams/{team.Logo}" : null;

        return teams;
    }
}