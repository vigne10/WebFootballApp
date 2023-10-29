using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Articles.UpdateArticle;

public class UpdateArticleCommand : IRequest<ErrorOr<Article>>
{
    [FromRoute] public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}