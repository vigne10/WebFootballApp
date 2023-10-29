using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Rewards.AddTeamReward;

public class AddTeamRewardCommandHandler : IRequestHandler<AddTeamRewardCommand, ErrorOr<Reward>>
{
    private readonly ApplicationDbContext _context;

    public AddTeamRewardCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Reward>> Handle(AddTeamRewardCommand request, CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.CompetitionSeasonId);
        if (competitionSeason == null) return Error.NotFound("Competition season not found");

        var team = await _context.Team.FindAsync(request.TeamId);
        if (team == null) return Error.NotFound("Team not found");

        var reward = new Reward
        {
            CompetitionSeason = competitionSeason,
            Team = team
        };

        _context.Reward.Add(reward);
        await _context.SaveChangesAsync(cancellationToken);

        return reward;
    }
}