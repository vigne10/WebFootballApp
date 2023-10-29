using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Common;
using WebFootballApp.Features.StaffMembers.CreateStaffMember;
using WebFootballApp.Features.StaffMembers.DeleteStaffMember;
using WebFootballApp.Features.StaffMembers.DeleteStaffMemberJob;
using WebFootballApp.Features.StaffMembers.DeleteStaffMemberPicture;
using WebFootballApp.Features.StaffMembers.DeleteStaffMemberTeam;
using WebFootballApp.Features.StaffMembers.ReadStaffMember;
using WebFootballApp.Features.StaffMembers.ReadStaffMembers;
using WebFootballApp.Features.StaffMembers.UpdateStaffMember;
using WebFootballApp.Features.StaffMembers.UpdateStaffMemberJob;
using WebFootballApp.Features.StaffMembers.UpdateStaffMemberTeam;
using WebFootballApp.Features.StaffMembers.UploadStaffMemberPicture;

namespace WebFootballApp.Features.StaffMembers;

[ApiController]
[Route("[controller]")]
public class StaffMembersController : ApiControllerBase
{
    [HttpGet("/api/staffMember/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Read([FromRoute] ReadStaffMemberCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }

    [HttpGet("/api/staffMembers")]
    public async Task<IActionResult> ReadAll([FromQuery] ReadStaffMembersCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMembers => Ok(staffMembers),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/staffMember")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create([FromBody] CreateStaffMemberCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }

    [HttpPost("/api/staffMember/{Id}/picture")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UploadLogo([FromForm] UploadStaffMemberPictureCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            picturePath => Ok(picturePath),
            errors => BadRequest(errors));
    }

    [HttpPut("/api/staffMember/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update([FromBody] UpdateStaffMemberCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }

    [HttpPatch("/api/staffMember/{Id}/job")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateJob([FromQuery] UpdateStaffMemberJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }

    [HttpPatch("/api/staffMember/{Id}/team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateTeam([FromQuery] UpdateStaffMemberTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }


    [HttpDelete("/api/staffMember/{Id}")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete([FromRoute] DeleteStaffMemberCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/staffMember/{Id}/picture")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteLogo([FromRoute] DeleteStaffMemberPictureCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            _ => NoContent(),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/staffMember/{Id}/job")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteJob([FromRoute] DeleteStaffMemberJobCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }

    [HttpDelete("/api/staffMember/{Id}/team")]
    [Authorize(Policy = "AdminPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteTeam([FromRoute] DeleteStaffMemberTeamCommand command)
    {
        var errorOrResult = await Mediator.Send(command);

        return errorOrResult.Match<IActionResult>(
            staffMember => Ok(staffMember),
            errors => BadRequest(errors));
    }
}