using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Articles.DeleteArticleImage;

public class DeleteArticleImageCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}