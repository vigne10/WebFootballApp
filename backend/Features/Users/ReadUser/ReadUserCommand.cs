using ErrorOr;
using MediatR;
using WebFootballApp.Responses;

namespace WebFootballApp.Features.Users.ReadUser;

public class ReadUserCommand : IRequest<ErrorOr<UserResponse>>
{
    public int Id { get; set; }
}