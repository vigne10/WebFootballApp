using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.UploadStaffMemberPicture;

public class UploadStaffMemberPictureCommandHandler : IRequestHandler<UploadStaffMemberPictureCommand, ErrorOr<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public UploadStaffMemberPictureCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<string>> Handle(UploadStaffMemberPictureCommand request,
        CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember.FindAsync(request.Id);
        if (staffMember == null) return Error.NotFound("StaffMember doesn't exist");

        // Delete the old image and upload the new image
        string imagePath = null;
        if (request.Picture != null)
        {
            // Delete the old image
            if (!string.IsNullOrEmpty(staffMember.Picture))
                await _fileService.DeleteImageAsync(staffMember.Picture, "Staff");

            // Upload the new image
            imagePath = await _fileService.SaveImageAsync(request.Picture, "Staff");
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            staffMember.Picture = imagePath;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return imagePath;
    }
}