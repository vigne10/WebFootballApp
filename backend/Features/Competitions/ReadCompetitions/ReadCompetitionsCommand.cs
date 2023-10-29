using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Competitions.ReadCompetitions;

public class ReadCompetitionsCommand : IRequest<ErrorOr<List<Competition>>>
{
}