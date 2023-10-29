using System.Globalization;
using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.StaffMembers.CreateStaffMember;

public class CreateStaffMemberCommandHandler : IRequestHandler<CreateStaffMemberCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;
    
    public CreateStaffMemberCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<StaffMember>> Handle(CreateStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staffMember = new StaffMember();

        if (!string.IsNullOrEmpty(request.Birthday))
        {
            if (DateTime.TryParseExact(request.Birthday, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
                staffMember.Birthday = birthday;
            else
                return Error.Failure("Invalid Birthday");
        }

        staffMember.Name = request.Name;
        staffMember.Surname = request.Surname;

        _context.StaffMember.Add(staffMember);
        await _context.SaveChangesAsync(cancellationToken);

        return staffMember;
    }
}