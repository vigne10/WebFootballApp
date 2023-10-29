using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Players.UpdatePlayer;

public class UpdatePlayerCommand : IRequest<ErrorOr<Player>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Birthday { get; set; }
}