using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.UpdatePlayerTeam;

public class UpdatePlayerTeamCommand : IRequest<ErrorOr<Player>>
{
    [FromRoute] public int Id { get; set; }
    public int TeamId { get; set; }
}