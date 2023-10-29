using ErrorOr;
using MediatR;
using WebFootballApp.Common.Services;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberPicture;

public class DeleteStaffMemberPictureCommandHandler : IRequestHandler<DeleteStaffMemberPictureCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public DeleteStaffMemberPictureCommandHandler(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteStaffMemberPictureCommand request,
        CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember.FindAsync(request.Id);

        if (staffMember == null) return Error.NotFound("StaffMember not found");

        if (!string.IsNullOrEmpty(staffMember.Picture))
        {
            await _fileService.DeleteImageAsync(staffMember.Picture, "Staff");
            staffMember.Picture = null;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}