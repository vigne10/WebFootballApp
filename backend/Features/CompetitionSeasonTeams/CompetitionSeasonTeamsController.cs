using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.CompetitionSeasonTeams.AddTeamToCompetitionSeason;
using WebFootballApp.Features.CompetitionSeasonTeams.DeleteTeamToCompetitionSeason;
using WebFootballApp.Features.CompetitionSeasonTeams.ReadTeamsForCompetitionSeason;

namespace WebFootballApp.Features.CompetitionSeasonTeams;

[ApiController]
[Route("[controller]")]
public class CompetitionSeasonTeamsController : ApiControllerBase
{
    [HttpPost("/api/competition-season-team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddTeamToCompetitionSeason([FromQuery] AddTeamToCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competitionSeasonTeam => Ok(competitionSeasonTeam),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/competition-season-team/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteTeamToCompetitionSeason(
        [FromRoute] DeleteTeamToCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/competition-season/teams")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadTeamsForCompetitionSeason(
        [FromQuery] ReadTeamsForCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            teams => Ok(teams),
            errors => BadRequest(errors));
    }
    
}