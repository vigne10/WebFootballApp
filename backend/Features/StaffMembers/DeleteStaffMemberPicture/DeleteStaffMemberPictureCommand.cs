using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberPicture;

public class DeleteStaffMemberPictureCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}