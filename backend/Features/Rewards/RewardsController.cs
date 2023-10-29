using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Rewards.DeleteTeamReward;
using WebFootballApp.Features.Rewards.AddTeamReward;
using WebFootballApp.Features.Rewards.ReadTeamRewards;

namespace WebFootballApp.Features.Rewards;

[ApiController]
[Route("[controller]")]
public class RewardsController : ApiControllerBase
{
    [HttpGet("/api/rewards/team/{TeamId}")]
    public async Task<IActionResult> ReadAllByTeam([FromRoute] ReadTeamRewardsCommand rewardsCommand)
    {
        var errorOrResult = await Mediator.Send(rewardsCommand);

        return errorOrResult.Match<IActionResult>(
            rewards => Ok(rewards),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/reward")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] AddTeamRewardCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            reward => Ok(reward),
            errors => BadRequest(errors));
    }


    [HttpDelete("/api/reward/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteTeamRewardCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}