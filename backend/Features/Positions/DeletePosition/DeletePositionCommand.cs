using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Positions.DeletePosition;

public class DeletePositionCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}