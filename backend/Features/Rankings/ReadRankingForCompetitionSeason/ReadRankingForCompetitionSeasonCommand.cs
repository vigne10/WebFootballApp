using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Rankings.ReadRankingForCompetitionSeason;

public class ReadRankingForCompetitionSeasonCommand : IRequest<ErrorOr<List<Ranking>>>
{
    public int CompetitionSeasonId { get; set; }
}