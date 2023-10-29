using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Players.CreatePlayer;
using WebFootballApp.Features.Players.DeletePlayer;
using WebFootballApp.Features.Players.DeletePlayerPicture;
using WebFootballApp.Features.Players.DeletePlayerPosition;
using WebFootballApp.Features.Players.DeletePlayerTeam;
using WebFootballApp.Features.Players.ReadPlayer;
using WebFootballApp.Features.Players.ReadPlayers;
using WebFootballApp.Features.Players.UpdatePlayer;
using WebFootballApp.Features.Players.UpdatePlayerPosition;
using WebFootballApp.Features.Players.UpdatePlayerTeam;
using WebFootballApp.Features.Players.UploadPlayerPicture;

namespace WebFootballApp.Features.Players;

[ApiController]
[Route("[controller]")]
public class PlayersController : ApiControllerBase
{
    [HttpGet("/api/player/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadPlayerCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/players")]
    public async Task<IActionResult> ReadAll([FromQuery] ReadPlayersCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            players => Ok(players),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/player")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreatePlayerCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/player/{Id}/picture")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadLogo([FromForm] UploadPlayerPictureCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            picturePath => Ok(picturePath),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/player/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdatePlayerCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpPatch("/api/player/{Id}/position")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdatePosition([FromQuery] UpdatePlayerPositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpPatch("/api/player/{Id}/team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateTeam([FromQuery] UpdatePlayerTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/player/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeletePlayerCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/player/{Id}/picture")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteLogo([FromRoute] DeletePlayerPictureCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/player/{Id}/position")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeletePosition([FromRoute] DeletePlayerPositionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/player/{Id}/team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteTeam([FromRoute] DeletePlayerTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            player => Ok(player),
            errors => BadRequest(errors));
    }
}