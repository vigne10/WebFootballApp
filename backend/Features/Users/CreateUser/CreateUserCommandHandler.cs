using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Enum;
using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserResponse>>
{
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Hash the password using Bcrypt
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        UserRole userRole;

        if (request.Role == "Admin")
            userRole = UserRole.Admin;
        else if (request.Role == "SuperAdmin")
            userRole = UserRole.SuperAdmin;
        else
            return Error.Failure("Invalid role");

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Address = request.Address,
            Mail = request.Mail,
            Password = hashedPassword, // Store the hashed password in database
            RegistrationDate = DateTime.Now,
            RoleId = userRole
        };

        _context.User.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

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