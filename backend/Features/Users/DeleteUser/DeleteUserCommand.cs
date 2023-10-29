using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Users.DeleteUser;

public class DeleteUserCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}