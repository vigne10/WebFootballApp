using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Players.DeletePlayerPicture;

public class DeletePlayerPictureCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}