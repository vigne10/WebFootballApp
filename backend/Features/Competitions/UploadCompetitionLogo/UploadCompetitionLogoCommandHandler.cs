using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.Competitions.UploadCompetitionLogo;

public class UploadCompetitionLogoCommandHandler : IRequestHandler<UploadCompetitionLogoCommand, ErrorOr<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public UploadCompetitionLogoCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadCompetitionLogoCommand request, CancellationToken cancellationToken)
    {
        var competition = await _context.Competition.FindAsync(request.Id);
        if (competition == null) return Error.NotFound("Competition doesn't exist");

        // Delete the old image and upload the new image
        string imagePath = null;
        if (request.Logo != null)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(competition.Logo))
                await _fileService.DeleteImageAsync(competition.Logo, "Competition");

            // Upload the new image
            imagePath = await _fileService.SaveImageAsync(request.Logo, "Competition");
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            competition.Logo = imagePath;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return imagePath;
    }
}