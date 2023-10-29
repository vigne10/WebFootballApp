using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Competitions.DeleteCompetition;

public class DeleteCompetitionCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}