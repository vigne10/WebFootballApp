using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebFootballApp.Features.Competitions.UploadCompetitionLogo;

public class UploadCompetitionLogoCommand : IRequest<ErrorOr<string>>
{
    [FromRoute] public int Id { get; set; }
    public IFormFile? Logo { get; set; }
}