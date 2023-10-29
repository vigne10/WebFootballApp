using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Players.DeletePlayer;

public class DeletePlayerCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}