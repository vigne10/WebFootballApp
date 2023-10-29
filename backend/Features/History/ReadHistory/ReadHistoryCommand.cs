using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.History.ReadHistory;

public class ReadHistoryCommand : IRequest<ErrorOr<Entities.History>>
{
    public int Id { get; set; }
}