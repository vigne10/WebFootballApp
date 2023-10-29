using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, ErrorOr<Team>>
{
    private readonly ApplicationDbContext _context;

    public CreateTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Team>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Name = request.Name
        };
        
        _context.Team.Add(team);
        await _context.SaveChangesAsync(cancellationToken);

        return team;
    }
}