using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Positions.CreatePosition;
using WebFootballApp.Features.Positions.DeletePosition;
using WebFootballApp.Features.Positions.ReadPosition;
using WebFootballApp.Features.Positions.ReadPositions;
using WebFootballApp.Features.Positions.UpdatePosition;

namespace WebFootballApp.Features.Positions;

[ApiController]
[Route("[controller]")]
public class PositionsController : ApiControllerBase
{
    [HttpGet("/api/position/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadPositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            position => Ok(position),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/positions")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadPositionsCommand());

        return errorOrResult.Match<IActionResult>(
            positions => Ok(positions),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/position")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreatePositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            position => Ok(position),
            errors => BadRequest(errors));
    }


    [HttpPut("/api/position/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdatePositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            position => Ok(position),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/position/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeletePositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}