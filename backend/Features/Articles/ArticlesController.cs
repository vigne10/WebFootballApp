using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Articles.CreateArticle;
using WebFootballApp.Features.Articles.DeleteArticle;
using WebFootballApp.Features.Articles.DeleteArticleImage;
using WebFootballApp.Features.Articles.ReadArticle;
using WebFootballApp.Features.Articles.ReadPaginatedArticles;
using WebFootballApp.Features.Articles.UpdateArticle;
using WebFootballApp.Features.Articles.UploadArticleImage;
using WebFootballApp.Requests;

namespace WebFootballApp.Features.Articles;

[ApiController]
[Route("[controller]")]
public class ArticlesController : ApiControllerBase
{
    [HttpGet("/api/articles")]
    public async Task<IActionResult> ReadAll([FromQuery] PaginationRequest paginationRequest)
    {
        var command = new ReadPaginatedArticlesCommand { PaginationRequest = paginationRequest };
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            pagedResponse => Ok(pagedResponse),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/article/{Id}")]
    public async Task<IActionResult> Read([FromRoute] ReadArticleCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            articleResponse => Ok(articleResponse),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/article")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateArticleCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            articleResponse => Ok(articleResponse),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/article/{Id}/image")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadImage([FromForm] UploadArticleImageCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            imagePath => Ok(imagePath),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/article/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateArticleCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            articleResponse => Ok(articleResponse),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/article/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteArticleCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/article/{Id}/image")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteImage([FromRoute] DeleteArticleImageCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}