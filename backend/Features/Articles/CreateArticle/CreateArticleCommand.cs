using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Articles.CreateArticle;

public class CreateArticleCommand : IRequest<ErrorOr<Article>>
{
    public string Title { get; set; }
    public string Content { get; set; }
}