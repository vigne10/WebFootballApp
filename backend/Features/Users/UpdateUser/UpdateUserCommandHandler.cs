using ErrorOr;
using MediatR;
using WebFootballApp.Enum;
using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UserResponse>>
{
    private readonly ApplicationDbContext _context;

    public UpdateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FindAsync(request.Id);

        if (user == null) return Error.NotFound();
        
        // Check if the user wants to change the password
        if (!string.IsNullOrEmpty(request.CurrentPassword) && !string.IsNullOrEmpty(request.NewPassword))
        {

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
                return Error.Failure("Invalid password.");

            // Hash the new password and update the user
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Password = hashedPassword;
            
        }

        // Update other properties if they are not null or empty
        if (!string.IsNullOrEmpty(request.Name)) user.Name = request.Name;
        if (!string.IsNullOrEmpty(request.Surname)) user.Surname = request.Surname;
        if (!string.IsNullOrEmpty(request.Address)) user.Address = request.Address;
        if (!string.IsNullOrEmpty(request.Mail)) user.Mail = request.Mail;
        if (!string.IsNullOrEmpty(request.Role))
        {
            if (request.Role == "Admin")
                user.RoleId = UserRole.Admin;
            else if (request.Role == "SuperAdmin")
                user.RoleId = UserRole.SuperAdmin;
            else
                return Error.Failure("Invalid role");
        }

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