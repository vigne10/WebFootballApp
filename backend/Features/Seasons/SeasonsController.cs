using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Seasons.CreateSeason;
using WebFootballApp.Features.Seasons.DeleteSeason;
using WebFootballApp.Features.Seasons.ReadSeason;
using WebFootballApp.Features.Seasons.ReadSeasons;
using WebFootballApp.Features.Seasons.UpdateSeason;

namespace WebFootballApp.Features.Seasons;

[ApiController]
[Route("[controller]")]
public class SeasonsController : ApiControllerBase
{
    [HttpGet("/api/season/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            season => Ok(season),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/seasons")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadSeasonsCommand());

        return errorOrResult.Match<IActionResult>(
            seasons => Ok(seasons),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/season")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            season => Ok(season),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/season/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            season => Ok(season),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/season/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}