using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.DeleteArticle;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteArticleCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.Article.FindAsync(request.Id);

        if (article == null) return Error.NotFound("Article not found");

        _context.Article.Remove(article);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}