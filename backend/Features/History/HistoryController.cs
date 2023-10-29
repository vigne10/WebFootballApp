using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.History.CreateHistory;
using WebFootballApp.Features.History.DeleteHistory;
using WebFootballApp.Features.History.ReadHistory;
using WebFootballApp.Features.History.UpdateHistory;

namespace WebFootballApp.Features.History;

[ApiController]
[Route("[controller]")]
public class HistoryController : ApiControllerBase
{
    [HttpGet("/api/history/{Id}")]
    public async Task<IActionResult> Read([FromRoute] ReadHistoryCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            history => Ok(history),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/history")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateHistoryCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            history => Ok(history),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/history/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateHistoryCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            history => Ok(history),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/history/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteHistoryCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}