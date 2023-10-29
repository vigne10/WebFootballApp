using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.DeleteTeamLogo;

public class DeleteTeamLogoCommandHandler : IRequestHandler<DeleteTeamLogoCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public DeleteTeamLogoCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteTeamLogoCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.Id);

        if (team == null) return Error.NotFound("Team not found");

        if (!string.IsNullOrEmpty(team.Logo))
        {
            await _fileService.DeleteImageAsync(team.Logo, "Team");
            team.Logo = null;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}