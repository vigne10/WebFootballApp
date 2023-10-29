using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Teams.ReadTeams;

public class ReadTeamsCommand : IRequest<ErrorOr<List<Team>>>
{
}