using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Articles.DeleteArticle;

public class DeleteArticleCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}