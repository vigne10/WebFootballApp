using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.UpdatePlayerPosition;

public class UpdatePlayerPositionCommand : IRequest<ErrorOr<Player>>
{
    [FromRoute] public int Id { get; set; }
    public int PositionId { get; set; }
}