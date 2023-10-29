using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Competitions.CreateCompetition;

public class CreateCompetitionCommand : IRequest<ErrorOr<Competition>>
{
    public string Name { get; set; }
}