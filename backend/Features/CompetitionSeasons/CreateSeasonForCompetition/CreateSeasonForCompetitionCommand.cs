using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasons.CreateSeasonForCompetition;

public class CreateSeasonForCompetitionCommand : IRequest<ErrorOr<CompetitionSeason>>
{
    public int CompetitionId { get; set; }
    public int SeasonId { get; set; }
}