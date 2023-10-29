using ErrorOr;
using MediatR;
using WebFootballApp.Responses;

namespace WebFootballApp.Features.Users.ReadUsers;

public class ReadUsersCommand : IRequest<ErrorOr<List<UserResponse>>>
{
}