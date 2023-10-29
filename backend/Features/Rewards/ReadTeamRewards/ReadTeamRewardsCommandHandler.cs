using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Rewards.ReadTeamRewards;

public class ReadTeamRewardsCommandHandler : IRequestHandler<ReadTeamRewardsCommand, ErrorOr<List<Reward>>>
{
    private readonly ApplicationDbContext _context;

    public ReadTeamRewardsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Reward>>> Handle(ReadTeamRewardsCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.TeamId);
        if (team == null) return Error.NotFound("Team not found");

        var rewards = await _context.Reward
            .Include(r => r.CompetitionSeason)
            .Include(r => r.CompetitionSeason.Competition)
            .Include(r => r.CompetitionSeason.Season)
            .Where(r => r.Team == team)
            .OrderByDescending(r => r.CompetitionSeason.Season.Name)
            .ToListAsync(cancellationToken);

        return rewards;
    }
}