using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Matches.CreateMatch;
using WebFootballApp.Features.Matches.DeleteMatch;
using WebFootballApp.Features.Matches.ReadMatch;
using WebFootballApp.Features.Matches.ReadMatchesForCompetitionSeason;
using WebFootballApp.Features.Matches.ReadTeamMatchesForSeason;
using WebFootballApp.Features.Matches.ResetMatchScores;
using WebFootballApp.Features.Matches.UpdateMatch;

namespace WebFootballApp.Features.Matches;

[ApiController]
[Route("[controller]")]
public class MatchesController : ApiControllerBase
{
    [HttpGet("/api/match/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadMatchCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            match => Ok(match),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/matches/competition-season/{CompetitionSeasonId}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadMatchesForCompetitionSeason(
        [FromRoute] ReadMatchesForCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            matches => Ok(matches),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/matches/team")]
    public async Task<IActionResult> ReadTeamMatchesForSeason([FromQuery] ReadTeamMatchesForSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            matches => Ok(matches),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/match")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateMatchCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            match => Ok(match),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/match/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateMatchCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            match => Ok(match),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/match/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteMatchCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/match/{Id}/reset-scores")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ResetMatchScores([FromRoute] ResetMatchScoresCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}