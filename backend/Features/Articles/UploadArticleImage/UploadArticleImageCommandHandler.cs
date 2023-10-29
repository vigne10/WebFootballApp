using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.UploadArticleImage;

public class UploadArticleImageCommandHandler : IRequestHandler<UploadArticleImageCommand, ErrorOr<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public UploadArticleImageCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadArticleImageCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.Article.FindAsync(request.Id);
        if (article == null) return Error.Failure("Article doesn't exist");

        // Delete the old image and upload the new image
        string imagePath = null;
        if (request.Image != null)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(article.Image))
            {
                await _fileService.DeleteImageAsync(article.Image, "Article");
            }
            
            // Upload the new image
            imagePath = await _fileService.SaveImageAsync(request.Image, "Article");
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            article.Image = imagePath;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return imagePath;
    }
}