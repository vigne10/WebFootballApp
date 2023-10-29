using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebFootballApp.Responses;

namespace WebFootballApp.Features.Users.UpdateUser;

public class UpdateUserCommand : IRequest<ErrorOr<UserResponse>>
{
    [FromRoute] public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Address { get; set; }
    public string? Mail { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string Role { get; set; }
}