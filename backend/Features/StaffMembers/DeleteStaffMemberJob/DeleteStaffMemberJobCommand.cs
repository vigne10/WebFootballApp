using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberJob;

public class DeleteStaffMemberJobCommand : IRequest<ErrorOr<StaffMember>>
{
    public int Id { get; set; }
}