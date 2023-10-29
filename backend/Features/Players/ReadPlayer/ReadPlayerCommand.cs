using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.ReadPlayer;

public class ReadPlayerCommand : IRequest<ErrorOr<Player>>
{
    public int Id { get; set; }
}