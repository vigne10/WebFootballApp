using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Players.DeletePlayerPicture;

public class DeletePlayerPictureCommandHandler : IRequestHandler<DeletePlayerPictureCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public DeletePlayerPictureCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePlayerPictureCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Player.FindAsync(request.Id);

        if (player == null) return Error.NotFound("Player not found");

        if (!string.IsNullOrEmpty(player.Picture))
        {
            await _fileService.DeleteImageAsync(player.Picture, "Player");
            player.Picture = null;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}