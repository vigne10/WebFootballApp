using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ErrorOr<Article>>
{
    private readonly ApplicationDbContext _context;

    public UpdateArticleCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Article>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.Article.SingleOrDefaultAsync(a => a.Id == request.Id);

        if (article == null) return Error.NotFound("Article not found");
        
        if (!string.IsNullOrEmpty(request.Title))
        {
            article.Title = request.Title;
        }
        if (!string.IsNullOrEmpty(request.Content))
        {
            article.Content = request.Content;
        }
        
        await _context.SaveChangesAsync(cancellationToken);

        return article;
    }
}