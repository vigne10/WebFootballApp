using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.ReadStaffMembers;

public class
    ReadStaffMembersCommandHandler : IRequestHandler<ReadStaffMembersCommand, ErrorOr<List<StaffMember>>>
{
    private readonly ApplicationDbContext _context;

    public ReadStaffMembersCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<StaffMember>>> Handle(ReadStaffMembersCommand request,
        CancellationToken cancellationToken)
    {
        var team = await _context.Team.FindAsync(request.TeamId);

        if (team == null) return Error.NotFound("Team not found");

        var staffMembers = await _context.StaffMember
            .Include(s => s.Job)
            .Where(s => s.Team == team)
            .OrderBy(s => s.Surname) // Sorting by position name
            .ToListAsync(cancellationToken);

        return staffMembers;
    }
}