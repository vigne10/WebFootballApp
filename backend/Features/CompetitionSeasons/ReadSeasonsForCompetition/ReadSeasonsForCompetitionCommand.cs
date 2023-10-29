using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasons.ReadSeasonsForCompetition;

public class ReadSeasonsForCompetitionCommand : IRequest<ErrorOr<List<CompetitionSeason>>>
{
    public int CompetitionId { get; set; }
}