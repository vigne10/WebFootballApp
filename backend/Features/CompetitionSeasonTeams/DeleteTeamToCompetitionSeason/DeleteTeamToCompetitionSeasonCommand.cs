using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.CompetitionSeasonTeams.DeleteTeamToCompetitionSeason;

public class DeleteTeamToCompetitionSeasonCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}