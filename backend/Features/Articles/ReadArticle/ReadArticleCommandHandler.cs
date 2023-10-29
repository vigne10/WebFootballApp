using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.ReadArticle;

public class ReadArticleCommandHandler : IRequestHandler<ReadArticleCommand, ErrorOr<Article>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadArticleCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Article>> Handle(ReadArticleCommand request, CancellationToken cancellationToken)
    {
        var article = _context.Article.SingleOrDefault(a => a.Id == request.Id);

        if (article == null) return Error.NotFound("Article not found");

        if (article.Image != null)
            article.Image = $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Articles/{article.Image}";

        return article;
    }
}