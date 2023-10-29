using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Requests;
using WebFootballApp.Responses;

namespace WebFootballApp.Features.Articles.ReadPaginatedArticles;

public class ReadPaginatedArticlesCommand : IRequest<ErrorOr<PagedResponse<List<ArticleResponse>>>>
{
 public PaginationRequest PaginationRequest { get; set; }   
}