using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Matches.DeleteMatch;

public class DeleteMatchCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}