using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.CompetitionSeasons.CreateSeasonForCompetition;
using WebFootballApp.Features.CompetitionSeasons.DeleteSeasonForCompetition;
using WebFootballApp.Features.CompetitionSeasons.ReadAllCompetitionsSeasons;
using WebFootballApp.Features.CompetitionSeasons.ReadCompetitionSeason;
using WebFootballApp.Features.CompetitionSeasons.ReadSeasonsForCompetition;

namespace WebFootballApp.Features.CompetitionSeasons;

[ApiController]
[Route("[controller]")]
public class CompetitionSeasonsController : ApiControllerBase
{
    [HttpGet("/api/competition-season/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadCompetitionSeason([FromRoute] ReadCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competitionSeason => Ok(competitionSeason),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/competition/{CompetitionId}/seasons")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadSeasonsForCompetition([FromRoute] ReadSeasonsForCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competitionSeasons => Ok(competitionSeasons),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/competitions-seasons")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAllCompetitionsSeasons()
    {
        var errorOrResult = await Mediator.Send(new ReadAllCompetitionsSeasonsCommand());

        return errorOrResult.Match<IActionResult>(
            competitionSeasons => Ok(competitionSeasons),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/competition-season")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateSeasonForCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competitionSeason => Ok(competitionSeason),
            errors => BadRequest(errors));
    }


    [HttpDelete("/api/competition-season/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteSeasonForCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}