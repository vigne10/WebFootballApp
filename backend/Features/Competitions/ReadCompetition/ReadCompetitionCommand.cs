using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Competitions.ReadCompetition;

public class ReadCompetitionCommand : IRequest<ErrorOr<Competition>>
{
    public int Id { get; set; }
}