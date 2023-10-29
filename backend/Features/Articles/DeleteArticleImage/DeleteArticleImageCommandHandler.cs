using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.DeleteArticleImage;

public class DeleteArticleImageCommandHandler : IRequestHandler<DeleteArticleImageCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public DeleteArticleImageCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteArticleImageCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.Article.FindAsync(request.Id);

        if (article == null) return Error.NotFound("Article not found");

        if (!string.IsNullOrEmpty(article.Image))
        {
            await _fileService.DeleteImageAsync(article.Image, "Article");
            article.Image = null;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}