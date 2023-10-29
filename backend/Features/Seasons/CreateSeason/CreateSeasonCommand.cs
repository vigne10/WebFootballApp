using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Seasons.CreateSeason;

public class CreateSeasonCommand : IRequest<ErrorOr<Season>>
{
    public string Name { get; set; }
}