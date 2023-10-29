using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.CreateStaffMember;

public class CreateStaffMemberCommand : IRequest<ErrorOr<StaffMember>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Birthday { get; set; }
}