using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.CreateCompetition;

public class CreateCompetitionCommandHandler : IRequestHandler<CreateCompetitionCommand, ErrorOr<Competition>>
{
    private readonly ApplicationDbContext _context;

    public CreateCompetitionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Competition>> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = new Competition
        {
            Name = request.Name
        };
        
        _context.Competition.Add(competition);
        await _context.SaveChangesAsync(cancellationToken);

        return competition;
    }
}