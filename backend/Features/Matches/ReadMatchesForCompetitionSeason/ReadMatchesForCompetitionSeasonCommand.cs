using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Matches.ReadMatchesForCompetitionSeason;

public class ReadMatchesForCompetitionSeasonCommand : IRequest<ErrorOr<List<Match>>>
{
    public int CompetitionSeasonId { get; set; }
}