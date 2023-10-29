using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMemberJob;

public class UpdateStaffMemberJobCommand : IRequest<ErrorOr<StaffMember>>
{
    [FromRoute] public int Id { get; set; }
    public int JobId { get; set; }
}