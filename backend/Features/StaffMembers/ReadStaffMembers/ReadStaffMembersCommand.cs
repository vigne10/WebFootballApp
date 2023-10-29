using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.ReadStaffMembers;

public class ReadStaffMembersCommand : IRequest<ErrorOr<List<StaffMember>>>
{
    public int TeamId { get; set; }
}