using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.CompetitionSeasons.ReadAllCompetitionsSeasons;

public class
    ReadAllCompetitionsSeasonsCommandHandler : IRequestHandler<ReadAllCompetitionsSeasonsCommand,
        ErrorOr<List<CompetitionSeason>>>
{
    private readonly ApplicationDbContext _context;

    public ReadAllCompetitionsSeasonsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<CompetitionSeason>>> Handle(ReadAllCompetitionsSeasonsCommand request,
        CancellationToken cancellationToken)
    {
        var competitionSeasons = await _context.CompetitionSeason
            .Include(cs => cs.Competition)
            .Include(cs => cs.Season)
            .ToListAsync(cancellationToken);
        
        if (competitionSeasons == null) return Error.NotFound("Competition seasons not found");

        return competitionSeasons;
    }
}