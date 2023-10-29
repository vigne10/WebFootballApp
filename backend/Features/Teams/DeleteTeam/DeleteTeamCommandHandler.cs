using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public DeleteTeamCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.Id);

        if (team == null) return Error.NotFound("Team not found");

        var competitionSeasonTeams = await _context.CompetitionSeasonTeam
            .Include(cst => cst.CompetitionSeason)
            .Include(cst => cst.CompetitionSeason.Competition)
            .Include(cst => cst.CompetitionSeason.Season)
            .Include(cst => cst.Team)
            .Where(cst => cst.Team.Id == request.Id)
            .ToListAsync(cancellationToken);

        var competitionSeasons = competitionSeasonTeams
            .Select(competitionSeasonTeam => competitionSeasonTeam.CompetitionSeason).ToList();

        _context.CompetitionSeasonTeam.RemoveRange(competitionSeasonTeams);

        var rankings = await _context.Ranking
            .Include(r => r.CompetitionSeason)
            .Include(r => r.Team)
            .Where(r => r.Team.Id == team.Id)
            .ToListAsync(cancellationToken);

        _context.Ranking.RemoveRange(rankings);

        var rewards = await _context.Reward
            .Include(r => r.CompetitionSeason)
            .Include(r => r.Team)
            .Where(r => r.Team.Id == team.Id)
            .ToListAsync(cancellationToken);

        _context.Reward.RemoveRange(rewards);

        var matches = await _context.Match
            .Include(m => m.CompetitionSeason)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.HomeTeam.Id == team.Id || m.AwayTeam.Id == team.Id)
            .ToListAsync(cancellationToken);

        _context.Match.RemoveRange(matches);

        var players = await _context.Player
            .Include(p => p.Team)
            .Where(p => p.Team != null && p.Team.Id == team.Id)
            .ToListAsync(cancellationToken);

        foreach (var player in players) player.Team = null;

        _context.Player.UpdateRange(players);

        var staffMembers = await _context.StaffMember
            .Include(p => p.Team)
            .Where(p => p.Team != null && p.Team.Id == team.Id)
            .ToListAsync(cancellationToken);

        foreach (var staffMember in staffMembers)
            staffMember.Team = null;

        _context.StaffMember.UpdateRange(staffMembers);

        _context.Team.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var competitionSeason in competitionSeasons)
            await _rankingServices.UpdateRankingsAsync(competitionSeason);

        return Unit.Value;
    }
}