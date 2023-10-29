using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.CompetitionSeasons.DeleteSeasonForCompetition;

public class DeleteSeasonForCompetitionCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}