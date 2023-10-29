using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.DeletePlayerTeam;

public class DeletePlayerTeamCommand : IRequest<ErrorOr<Player>>
{
    public int Id { get; set; }
}