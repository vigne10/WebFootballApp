using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Positions.CreatePosition;

public class CreatePositionCommand : IRequest<ErrorOr<Position>>
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
}