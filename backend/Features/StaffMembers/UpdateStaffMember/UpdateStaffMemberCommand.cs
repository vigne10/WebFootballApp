using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMember;

public class UpdateStaffMemberCommand : IRequest<ErrorOr<StaffMember>>
{
    [FromRoute] public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Birthday { get; set; }
}