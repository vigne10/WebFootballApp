using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Positions.ReadPositions;

public class ReadPositionsCommand : IRequest<ErrorOr<List<Position>>>
{
}