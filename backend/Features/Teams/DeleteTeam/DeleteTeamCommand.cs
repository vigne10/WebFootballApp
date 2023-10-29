using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Teams.DeleteTeam;

public class DeleteTeamCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}