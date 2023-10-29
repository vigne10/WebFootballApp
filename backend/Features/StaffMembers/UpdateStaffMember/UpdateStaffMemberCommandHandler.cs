using System.Globalization;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.StaffMembers.UpdateStaffMember;

public class UpdateStaffMemberCommandHandler : IRequestHandler<UpdateStaffMemberCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;

    public UpdateStaffMemberCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<StaffMember>> Handle(UpdateStaffMemberCommand request,
        CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember
            .Include(s => s.Job)
            .Include(s => s.Team)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (staffMember == null) return Error.NotFound("StaffMember not found");

        if (!string.IsNullOrEmpty(request.Name)) staffMember.Name = request.Name;
        if (!string.IsNullOrEmpty(request.Surname)) staffMember.Surname = request.Surname;

        if (request.Birthday != null && DateTime.TryParseExact(request.Birthday, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
            staffMember.Birthday = birthday;
        else
            staffMember.Birthday = null; // Put null if birthday is empty

        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}