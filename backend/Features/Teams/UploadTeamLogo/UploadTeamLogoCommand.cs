using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebFootballApp.Features.Teams.UploadTeamLogo;

public class UploadTeamLogoCommand : IRequest<ErrorOr<string>>
{
    [FromRoute] public int Id { get; set; }
    public IFormFile? Logo { get; set; }
}