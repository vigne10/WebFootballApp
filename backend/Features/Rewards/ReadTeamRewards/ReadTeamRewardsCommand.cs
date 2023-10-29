using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Rewards.ReadTeamRewards;

public class ReadTeamRewardsCommand : IRequest<ErrorOr<List<Reward>>>
{
    public int TeamId { get; set; }
}