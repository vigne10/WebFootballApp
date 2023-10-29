using System.Security.Claims;
using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ErrorOr<Article>>
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateArticleCommandHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<Article>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        // Get the user id from the token
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Error.Failure("User not found");

        var userId = int.Parse(userIdClaim.Value);
        var user = await _context.User.FindAsync(userId);
        if (user == null) return Error.Failure("User not found");

        var article = new Article
        {
            User = user,
            Title = request.Title,
            Content = request.Content,
            Date = DateTime.Now
        };

        _context.Article.Add(article);
        await _context.SaveChangesAsync(cancellationToken);

        var articleResponse = new Article
        {
            Id = article.Id,
            User = article.User,
            Title = article.Title,
            Content = article.Content,
            Date = article.Date
        };

        return articleResponse;
    }
}