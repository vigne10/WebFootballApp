using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMemberJob;

public class UpdateStaffMemberJobCommandHandler : IRequestHandler<UpdateStaffMemberJobCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;

    public UpdateStaffMemberJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<StaffMember>> Handle(UpdateStaffMemberJobCommand request,
        CancellationToken cancellationToken)
    {
        var job = await _context.Job.FindAsync(request.JobId);
        if (job == null) return Error.NotFound("Job not found");

        var staffMember = await _context.StaffMember.Include(s => s.Job).Include(s => s.Team)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (staffMember == null) return Error.NotFound("StaffMember not found");

        staffMember.Job = job;

        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}