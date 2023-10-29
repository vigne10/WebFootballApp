using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Teams.ReadTeam;

public class ReadTeamCommand : IRequest<ErrorOr<Team>>
{
    public int Id { get; set; }
}