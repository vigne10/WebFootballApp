using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Positions.UpdatePosition;

public class UpdatePositionCommand : IRequest<ErrorOr<Position>>
{
    [FromRoute] public int Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
}