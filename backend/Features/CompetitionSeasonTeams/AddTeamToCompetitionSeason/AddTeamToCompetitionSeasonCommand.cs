using MediatR;
using ErrorOr;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasonTeams.AddTeamToCompetitionSeason;

public class AddTeamToCompetitionSeasonCommand : IRequest<ErrorOr<CompetitionSeasonTeam>>
{
    public int TeamId { get; set; }
    public int CompetitionSeasonId { get; set; }
}