using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberTeam;

public class DeleteStaffMemberTeamCommand : IRequest<ErrorOr<StaffMember>>
{
    public int Id { get; set; }
}