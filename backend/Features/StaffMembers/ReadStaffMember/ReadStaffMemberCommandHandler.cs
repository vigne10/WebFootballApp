using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.StaffMembers.ReadStaffMember;

public class ReadStaffMemberCommandHandler : IRequestHandler<ReadStaffMemberCommand, ErrorOr<StaffMember>>
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ReadStaffMemberCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<StaffMember>> Handle(ReadStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staffMember = _context.StaffMember
            .Include(s => s.Job)
            .Include(s => s.Team)
            .SingleOrDefault(s => s.Id == request.Id);

        if (staffMember == null) return Error.NotFound("StaffMember not found");
        
        if (staffMember.Picture != null)
            staffMember.Picture = $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/StaffMembers/{staffMember.Picture}";

        return staffMember;
    }
}