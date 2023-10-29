using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.ReadPlayers;

public class ReadPlayersCommand : IRequest<ErrorOr<List<Player>>>
{
    public int TeamId { get; set; }
}