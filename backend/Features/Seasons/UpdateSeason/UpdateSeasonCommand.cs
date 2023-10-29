using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Seasons.UpdateSeason;

public class UpdateSeasonCommand : IRequest<ErrorOr<Season>>
{
    [FromRoute] public int Id { get; set; }
    public string Name { get; set; }
}