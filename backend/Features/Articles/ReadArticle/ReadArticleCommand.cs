using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Articles.ReadArticle;

public class ReadArticleCommand : IRequest<ErrorOr<Article>>
{
    public int Id { get; set; }
}