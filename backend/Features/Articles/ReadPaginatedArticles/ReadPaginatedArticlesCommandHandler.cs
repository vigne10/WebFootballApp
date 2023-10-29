using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Articles.ReadPaginatedArticles;

public class
    ReadPaginatedArticlesCommandHandler : IRequestHandler<ReadPaginatedArticlesCommand,
        ErrorOr<PagedResponse<List<ArticleResponse>>>>
{
    private readonly ApplicationDbContext _context;

    public ReadPaginatedArticlesCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<PagedResponse<List<ArticleResponse>>>> Handle(ReadPaginatedArticlesCommand request,
        CancellationToken cancellationToken)
    {
        var pageSize = request.PaginationRequest.PageSize;
        var pageNumber = request.PaginationRequest.PageNumber;

        var totalArticles = await _context.Article.CountAsync();
        var articles = await _context.Article
            .Include(a => a.User)
            .OrderByDescending(a => a.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var articleResponses = articles.Select(article => new ArticleResponse
        {
            Id = article.Id,
            User = new UserResponse
            {
                Id = article.User.Id,
                Name = article.User.Name,
                Surname = article.User.Surname,
                Address = article.User.Address,
                Mail = article.User.Mail,
                RegistrationDate = article.User.RegistrationDate,
                Role = article.User.RoleId.ToString(),
                
            },
            Title = article.Title,
            Content = article.Content,
            Image = article.Image,
            Date = article.Date
        }).ToList();

        var totalPages = (int)Math.Ceiling((double)totalArticles / pageSize);

        var pagedResponse = new PagedResponse<List<ArticleResponse>>(articleResponses, totalArticles, totalPages);

        return pagedResponse;
    }
}