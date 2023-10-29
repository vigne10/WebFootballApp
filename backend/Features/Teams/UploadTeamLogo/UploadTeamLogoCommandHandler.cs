using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Teams.UploadTeamLogo;

public class UploadTeamLogoCommandHandler : IRequestHandler<UploadTeamLogoCommand, ErrorOr<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public UploadTeamLogoCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadTeamLogoCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.Id);
        if (team == null) return Error.NotFound("Team doesn't exist");

        // Delete the old image and upload the new image
        string imagePath = null;
        if (request.Logo != null)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(team.Logo))
                await _fileService.DeleteImageAsync(team.Logo, "Team");

            // Upload the new image
            imagePath = await _fileService.SaveImageAsync(request.Logo, "Team");
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            team.Logo = imagePath;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return imagePath;
    }
}