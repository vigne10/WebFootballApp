using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMember;

public class DeleteStaffMemberCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}