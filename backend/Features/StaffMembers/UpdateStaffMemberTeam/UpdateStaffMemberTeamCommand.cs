using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMemberTeam;

public class UpdateStaffMemberTeamCommand : IRequest<ErrorOr<StaffMember>>
{
    [FromRoute] public int Id { get; set; }
    public int TeamId { get; set; }
}