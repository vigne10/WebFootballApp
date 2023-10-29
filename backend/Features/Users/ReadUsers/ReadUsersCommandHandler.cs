using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Users.ReadUsers;

public class ReadUsersCommandHandler : IRequestHandler<ReadUsersCommand, ErrorOr<List<UserResponse>>>
{
    private readonly ApplicationDbContext _context;

    public ReadUsersCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<UserResponse>>> Handle(ReadUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _context.User
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Mail = user.Mail,
                RegistrationDate = user.RegistrationDate,
                Role = user.RoleId.ToString()
            })
            .ToListAsync(cancellationToken);

        return users;
    }
}