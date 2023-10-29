using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasons.ReadCompetitionSeason;

public class ReadCompetitionSeasonCommand : IRequest<ErrorOr<CompetitionSeason>>
{
    public int Id { get; set; }
}