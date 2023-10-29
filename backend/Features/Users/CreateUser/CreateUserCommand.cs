using ErrorOr;
using MediatR;
using WebFootballApp.Enum;
using WebFootballApp.Responses;

namespace WebFootballApp.Features.Users.CreateUser;

public class CreateUserCommand : IRequest<ErrorOr<UserResponse>>
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string? Address { get; set; }
    
    public string Mail { get; set; }
    
    public string Password { get; set; }

    public string Role { get; set; }

}