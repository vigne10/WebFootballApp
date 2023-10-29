using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Positions.ReadPosition;

public class ReadPositionCommand : IRequest<ErrorOr<Position>>
{
    public int Id { get; set; }
}