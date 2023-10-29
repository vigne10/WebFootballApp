using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebFootballApp.Features.StaffMembers.UploadStaffMemberPicture;

public class UploadStaffMemberPictureCommand : IRequest<ErrorOr<string>>
{
    [FromRoute] public int Id { get; set; }
    public IFormFile? Picture { get; set; }
}