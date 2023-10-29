using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberJob;

public class DeleteStaffMemberJobCommandHandler : IRequestHandler<DeleteStaffMemberJobCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;
    
    public DeleteStaffMemberJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<StaffMember>> Handle(DeleteStaffMemberJobCommand request, CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember
            .Include(s => s.Job)
            .Include(s => s.Team)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (staffMember == null) return Error.NotFound("StaffMember not found");

        staffMember.Job = null;

        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}