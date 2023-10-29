using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.DeletePlayerPosition;

public class DeletePlayerPositionCommand : IRequest<ErrorOr<Player>>
{
    public int Id { get; set; }
}