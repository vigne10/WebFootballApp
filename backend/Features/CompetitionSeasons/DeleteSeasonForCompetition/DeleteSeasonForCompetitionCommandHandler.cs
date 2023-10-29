using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.CompetitionSeasons.DeleteSeasonForCompetition;

public class
    DeleteSeasonForCompetitionCommandHandler : IRequestHandler<DeleteSeasonForCompetitionCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteSeasonForCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteSeasonForCompetitionCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeason = await _context.CompetitionSeason.FindAsync(request.Id);

        if (competitionSeason == null)
            return Error.NotFound("Competition season not found");

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
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}