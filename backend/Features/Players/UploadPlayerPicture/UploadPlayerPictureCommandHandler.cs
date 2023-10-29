using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.UploadPlayerPicture;

public class UploadPlayerPictureCommandHandler : IRequestHandler<UploadPlayerPictureCommand, ErrorOr<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public UploadPlayerPictureCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadPlayerPictureCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Player.FindAsync(request.Id);
        if (player == null) return Error.NotFound("Player doesn't exist");

        // Delete the old image and upload the new image
        string imagePath = null;
        if (request.Picture != null)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(player.Picture))
                await _fileService.DeleteImageAsync(player.Picture, "Player");

            // Upload the new image
            imagePath = await _fileService.SaveImageAsync(request.Picture, "Player");
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            player.Picture = imagePath;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return imagePath;
    }
}