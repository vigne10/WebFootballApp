using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.DeleteCompetitionLogo;

public class DeleteCompetitionLogoCommandHandler : IRequestHandler<DeleteCompetitionLogoCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public DeleteCompetitionLogoCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCompetitionLogoCommand request, CancellationToken cancellationToken)
    {
        var competition = await _context.Competition.FindAsync(request.Id);

        if (competition == null) return Error.NotFound("Competition not found");

        if (!string.IsNullOrEmpty(competition.Logo))
        {
            await _fileService.DeleteImageAsync(competition.Logo, "Competition");
            competition.Logo = null;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}