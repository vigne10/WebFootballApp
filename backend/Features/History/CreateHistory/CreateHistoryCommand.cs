using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.History.CreateHistory;

public class CreateHistoryCommand : IRequest<ErrorOr<Entities.History>>
{
    public string Content { get; set; }
}