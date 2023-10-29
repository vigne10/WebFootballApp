using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.Users.CreateUser;
using WebFootballApp.Features.Users.DeleteUser;
using WebFootballApp.Features.Users.ReadUser;
using WebFootballApp.Features.Users.ReadUsers;
using WebFootballApp.Features.Users.UpdateUser;

namespace WebFootballApp.Features.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ApiControllerBase
{
    [HttpGet("/api/user/{Id}")]
    [Authorize(Policy = "SuperAdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadUserCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            userResponse => Ok(userResponse),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/users")]
    [Authorize(Policy = "SuperAdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ReadAll()
    {
        var errorOrResult = await Mediator.Send(new ReadUsersCommand());

        return errorOrResult.Match<IActionResult>(
            users => Ok(users),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/user")]
    [Authorize(Policy = "SuperAdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            userResponse => Ok(userResponse),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/user/{Id}")]
    [Authorize(Policy = "SuperAdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            userResponse => Ok(userResponse),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/user/{Id}")]
    [Authorize(Policy = "SuperAdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }
}