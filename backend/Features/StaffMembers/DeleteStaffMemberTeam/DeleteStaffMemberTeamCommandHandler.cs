using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMemberTeam;

public class DeleteStaffMemberTeamCommandHandler : IRequestHandler<DeleteStaffMemberTeamCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;

    public DeleteStaffMemberTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<StaffMember>> Handle(DeleteStaffMemberTeamCommand request,
        CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember
            .Include(s => s.Job)
            .Include(s => s.Team)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (staffMember == null) return Error.NotFound("StaffMember not found");

        staffMember.Team = null;

        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}