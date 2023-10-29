using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Competitions.UpdateCompetition;

public class UpdateCompetitionCommand : IRequest<ErrorOr<Competition>>
{
    [FromRoute] public int Id { get; set; }
    public string Name { get; set; }
}