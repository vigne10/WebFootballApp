using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.UpdateTeam;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, ErrorOr<Team>>
{
    private readonly ApplicationDbContext _context;

    public UpdateTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Team>> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.Id);

        if (team == null) return Error.NotFound("Team not found");

        team.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return team;
    }
}