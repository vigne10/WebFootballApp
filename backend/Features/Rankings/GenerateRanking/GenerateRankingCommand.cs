using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Rankings.GenerateRanking;

public class GenerateRankingCommand : IRequest<ErrorOr<Unit>>
{
    public int CompetitionSeasonId { get; set; }
}