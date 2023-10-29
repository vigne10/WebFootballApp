using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Teams.CreateTeam;

public class CreateTeamCommand : IRequest<ErrorOr<Team>>
{
    public string Name { get; set; }
}