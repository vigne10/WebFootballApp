using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasonTeams.ReadTeamsForCompetitionSeason;

public class ReadTeamsForCompetitionSeasonCommand : IRequest<ErrorOr<List<CompetitionSeasonTeam>>>
{
    public int CompetitionSeasonId { get; set; }
}