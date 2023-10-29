using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.StaffMembers.ReadStaffMember;

public class ReadStaffMemberCommand : IRequest<ErrorOr<StaffMember>>
{
    [FromRoute] public int Id { get; set; }
}