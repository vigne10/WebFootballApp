using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace WebFootballApp.Features.Articles.UploadArticleImage;

public class UploadArticleImageCommand : IRequest<ErrorOr<string>>
{
    [FromRoute] public int Id { get; set; }
    public IFormFile? Image { get; set; }
}