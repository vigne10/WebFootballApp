using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Seasons.ReadSeason;

public class ReadSeasonCommand : IRequest<ErrorOr<Season>>
{
    public int Id { get; set; }
}