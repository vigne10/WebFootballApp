using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Competitions.DeleteCompetitionLogo;

public class DeleteCompetitionLogoCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}