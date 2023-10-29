using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.CreatePlayer;

public class CreatePlayerCommand : IRequest<ErrorOr<Player>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Birthday { get; set; }

}