using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Matches.ResetMatchScores;

public class ResetMatchScoresCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}