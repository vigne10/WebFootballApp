using WebFootballApp.Infrastructure;
using WebFootballApp.Responses;

namespace WebFootballApp.Auth;

public class AuthServices
{
    private readonly ApplicationDbContext _context;

    public AuthServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserResponse? ValidateUser(string mail, string password)
    {
        // Search for the user
        var user = _context.User.FirstOrDefault(u => u.Mail == mail);

        if (user == null) return null; // User not found

        // Without hashing
        // if (user.Password == password) return true;
        // return false;

        // With hashing
        var isPasswordValid = VerifyPassword(password, user.Password);
        if (isPasswordValid)
        {
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

        return null;
    }

    private bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }
}