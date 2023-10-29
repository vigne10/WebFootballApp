using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasonTeams.DeleteTeamToCompetitionSeason;

public class
    DeleteTeamToCompetitionSeasonCommandHandler : IRequestHandler<DeleteTeamToCompetitionSeasonCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly RankingServices _rankingServices;

    public DeleteTeamToCompetitionSeasonCommandHandler(ApplicationDbContext context, RankingServices rankingServices)
    {
        _context = context;
        _rankingServices = rankingServices;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteTeamToCompetitionSeasonCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeasonTeam = await _context.CompetitionSeasonTeam
            .Include(cst => cst.CompetitionSeason)
            .Include(cst => cst.CompetitionSeason.Competition)
            .Include(cst => cst.CompetitionSeason.Season)
            .Include(cst => cst.Team)
            .FirstOrDefaultAsync(cst => cst.Id == request.Id, cancellationToken);

        if (competitionSeasonTeam == null) return Error.NotFound("Competition season team not found");

        var competitionSeason = competitionSeasonTeam.CompetitionSeason;

        var rankings = await _context.Ranking
            .Include(r => r.CompetitionSeason)
            .Include(r => r.Team)
            .Where(r => r.Team.Id == competitionSeasonTeam.Team.Id && r.CompetitionSeason.Id == competitionSeason.Id)
            .ToListAsync(cancellationToken);

        _context.Ranking.RemoveRange(rankings);

        var rewards = await _context.Reward
            .Include(r => r.CompetitionSeason)
            .Include(r => r.Team)
            .Where(r => r.Team.Id == competitionSeasonTeam.Team.Id &&
                        r.CompetitionSeason.Id == competitionSeasonTeam.CompetitionSeason.Id)
            .ToListAsync(cancellationToken);

        _context.Reward.RemoveRange(rewards);

        var matches = await _context.Match
            .Include(m => m.CompetitionSeason)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => (m.HomeTeam.Id == competitionSeasonTeam.Team.Id &&
                         m.CompetitionSeason.Id == competitionSeasonTeam.CompetitionSeason.Id) ||
                        (m.AwayTeam.Id == competitionSeasonTeam.Team.Id &&
                         m.CompetitionSeason.Id == competitionSeasonTeam.CompetitionSeason.Id))
            .ToListAsync(cancellationToken);

        _context.Match.RemoveRange(matches);


        _context.CompetitionSeasonTeam.Remove(competitionSeasonTeam);
        await _context.SaveChangesAsync(cancellationToken);
        await _rankingServices.UpdateRankingsAsync(competitionSeason);

        return Unit.Value;
    }
}