using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Teams.CreateTeam;
using WebFootballApp.Features.Teams.DeleteTeam;
using WebFootballApp.Features.Teams.DeleteTeamLogo;
using WebFootballApp.Features.Teams.ReadTeam;
using WebFootballApp.Features.Teams.ReadTeams;
using WebFootballApp.Features.Teams.UpdateTeam;
using WebFootballApp.Features.Teams.UploadTeamLogo;

namespace WebFootballApp.Features.Teams;

[ApiController]
[Route("[controller]")]
public class TeamsController : ApiControllerBase
{
    [HttpGet("/api/team/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            team => Ok(team),
            errors => BadRequest(errors));
    }
    
    [HttpGet("/api/teams")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadTeamsCommand());

        return errorOrResult.Match<IActionResult>(
            teams => Ok(teams),
            errors => BadRequest(errors));
    }
    
    [HttpPost("/api/team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            teamResponse => Ok(teamResponse),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/team/{Id}/logo")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadLogo([FromForm] UploadTeamLogoCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            logoPath => Ok(logoPath),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/team/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            teamResponse => Ok(teamResponse),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/team/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
    
    [HttpDelete("/api/team/{Id}/logo")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteLogo([FromRoute] DeleteTeamLogoCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}