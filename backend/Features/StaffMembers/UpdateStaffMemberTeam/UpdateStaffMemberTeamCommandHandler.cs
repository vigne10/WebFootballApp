using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMemberTeam;

public class UpdateStaffMemberTeamCommandHandler : IRequestHandler<UpdateStaffMemberTeamCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;

    public UpdateStaffMemberTeamCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<StaffMember>> Handle(UpdateStaffMemberTeamCommand request,
        CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.TeamId);
        if (team == null) return Error.NotFound("Team not found");

        var staffMember = await _context.StaffMember.Include(s => s.Job)
            .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (staffMember == null) return Error.NotFound("StaffMember not found");

        staffMember.Team = team;

        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}