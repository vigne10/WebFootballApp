using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Users.ReadUser;

public class ReadUserCommandHandler : IRequestHandler<ReadUserCommand, ErrorOr<UserResponse>>
{
    private readonly ApplicationDbContext _context;

    public ReadUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<UserResponse>> Handle(ReadUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.User.SingleOrDefaultAsync(u => u.Id == request.Id);

        if (user == null)
        {
            return Error.NotFound();
        }
        
        return new UserResponse
        {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Mail = user.Mail,
                RegistrationDate = user.RegistrationDate,
                Role = user.RoleId.ToString()
        };
        
    }
}