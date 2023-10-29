using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.DeleteCompetition;

public class DeleteCompetitionCommandHandler : IRequestHandler<DeleteCompetitionCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _context.Competition.FindAsync(request.Id);

        if (competition == null) return Error.NotFound("Competition not found");

        var competitionSeasons = await _context.CompetitionSeason
            .Include(cs => cs.Competition)
            .Include(cs => cs.Season)
            .Where(cs => cs.Competition.Id == competition.Id)
            .ToListAsync(cancellationToken);

        if (competitionSeasons.Count > 0)
            foreach (var competitionSeason in competitionSeasons)
            {
                var competitionSeasonTeams = await _context.CompetitionSeasonTeam
                    .Include(cst => cst.CompetitionSeason)
                    .Where(cst => cst.CompetitionSeason.Id == competitionSeason.Id)
                    .ToListAsync(cancellationToken);

                _context.CompetitionSeasonTeam.RemoveRange(competitionSeasonTeams);

                var rewards = await _context.Reward
                    .Include(r => r.CompetitionSeason)
                    .Where(r => r.CompetitionSeason.Id == competitionSeason.Id)
                    .ToListAsync(cancellationToken);

                _context.Reward.RemoveRange(rewards);

                var matches = await _context.Match
                    .Include(m => m.CompetitionSeason)
                    .Where(m => m.CompetitionSeason.Id == competitionSeason.Id)
                    .ToListAsync(cancellationToken);

                _context.Match.RemoveRange(matches);

                var rankings = await _context.Ranking
                    .Include(r => r.CompetitionSeason)
                    .Where(r => r.CompetitionSeason.Id == competitionSeason.Id)
                    .ToListAsync(cancellationToken);

                _context.Ranking.RemoveRange(rankings);

                _context.CompetitionSeason.Remove(competitionSeason);
            }

        _context.Competition.Remove(competition);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}