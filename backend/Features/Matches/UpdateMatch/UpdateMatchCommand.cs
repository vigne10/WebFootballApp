using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Matches.UpdateMatch;

public class UpdateMatchCommand : IRequest<ErrorOr<Match>>
{
    [FromRoute] public int Id { get; set; }
    public int CompetitionSeasonId { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public string? Schedule { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
}