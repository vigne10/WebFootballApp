using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Matches.ReadTeamMatchesForSeason;

public class ReadTeamMatchesForSeasonCommand : IRequest<ErrorOr<List<Match>>>
{
    public int TeamId { get; set; }
    public int SeasonId { get; set; }
}