using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Competitions.CreateCompetition;
using WebFootballApp.Features.Competitions.DeleteCompetition;
using WebFootballApp.Features.Competitions.DeleteCompetitionLogo;
using WebFootballApp.Features.Competitions.ReadCompetition;
using WebFootballApp.Features.Competitions.ReadCompetitions;
using WebFootballApp.Features.Competitions.UpdateCompetition;
using WebFootballApp.Features.Competitions.UploadCompetitionLogo;

namespace WebFootballApp.Features.Competitions;

[ApiController]
[Route("[controller]")]
public class CompetitionsController : ApiControllerBase
{
    [HttpGet("/api/competition/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competition => Ok(competition),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/competitions")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadCompetitionsCommand());

        return errorOrResult.Match<IActionResult>(
            competitions => Ok(competitions),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/competition")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            articleResponse => Ok(articleResponse),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/competition/{Id}/logo")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadLogo([FromForm] UploadCompetitionLogoCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            logoPath => Ok(logoPath),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/competition/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            competitionResponse => Ok(competitionResponse),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/competition/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteCompetitionCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/competition/{Id}/logo")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteLogo([FromRoute] DeleteCompetitionLogoCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}