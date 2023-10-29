using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Teams.DeleteTeamLogo;

public class DeleteTeamLogoCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}