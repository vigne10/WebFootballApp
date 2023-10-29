using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Rewards.DeleteTeamReward;

public class DeleteTeamRewardCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}