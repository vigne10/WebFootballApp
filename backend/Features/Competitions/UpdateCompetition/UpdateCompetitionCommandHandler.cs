using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.UpdateCompetition;

public class UpdateCompetitionCommandHandler : IRequestHandler<UpdateCompetitionCommand, ErrorOr<Competition>>
{
    private readonly ApplicationDbContext _context;

    public UpdateCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Competition>> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _context.Competition.FindAsync(request.Id);
        
        if (competition == null) return Error.NotFound("Competition not found");
        
        competition.Name = request.Name;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return competition;
    }
}