using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Jobs.CreateJob;
using WebFootballApp.Features.Jobs.DeleteJob;
using WebFootballApp.Features.Jobs.ReadJob;
using WebFootballApp.Features.Jobs.ReadJobs;
using WebFootballApp.Features.Jobs.UpdateJob;

namespace WebFootballApp.Features.Jobs;

[ApiController]
[Route("[controller]")]
public class JobsController : ApiControllerBase
{
    [HttpGet("/api/job/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            job => Ok(job),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/jobs")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadJobsCommand());

        return errorOrResult.Match<IActionResult>(
            jobs => Ok(jobs),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/job")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            job => Ok(job),
            errors => BadRequest(errors));
    }


    [HttpPut("/api/job/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            job => Ok(job),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/job/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}