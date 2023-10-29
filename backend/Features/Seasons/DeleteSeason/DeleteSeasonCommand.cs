using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Seasons.DeleteSeason;

public class DeleteSeasonCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}