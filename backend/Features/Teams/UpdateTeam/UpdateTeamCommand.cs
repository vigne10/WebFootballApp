using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Teams.UpdateTeam;

public class UpdateTeamCommand : IRequest<ErrorOr<Team>>
{
    [FromRoute] public int Id { get; set; }
    public string Name { get; set; }
}