using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Rankings.GenerateRanking;
using WebFootballApp.Features.Rankings.ReadRankingForCompetitionSeason;

namespace WebFootballApp.Features.Rankings;

[ApiController]
[Route("[controller]")]
public class RankingsController : ApiControllerBase
{
    [HttpGet("/api/ranking")]
    public async Task<IActionResult> ReadAllByCompetition([FromQuery] ReadRankingForCompetitionSeasonCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            rankings => Ok(rankings),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/ranking/competition-season/{CompetitionSeasonId}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GenerateRanking([FromRoute] GenerateRankingCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}