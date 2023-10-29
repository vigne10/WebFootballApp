using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Rewards.AddTeamReward;

public class AddTeamRewardCommand : IRequest<ErrorOr<Reward>>
{
    public int CompetitionSeasonId { get; set; }
    public int TeamId { get; set; }
}