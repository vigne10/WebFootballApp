using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.DeleteStaffMember;

public class DeleteStaffMemberCommandHandler : IRequestHandler<DeleteStaffMemberCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteStaffMemberCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staffMember = await _context.StaffMember.FindAsync(request.Id);

        if (staffMember == null) return Error.NotFound("StaffMember not found");

        _context.StaffMember.Remove(staffMember);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}