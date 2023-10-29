using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebFootballApp.Features.Players.UploadPlayerPicture;

public class UploadPlayerPictureCommand : IRequest<ErrorOr<string>>
{
    [FromRoute] public int Id { get; set; }
    public IFormFile? Picture { get; set; }
}