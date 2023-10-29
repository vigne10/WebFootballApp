using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.History.DeleteHistory;

public class DeleteHistoryCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}