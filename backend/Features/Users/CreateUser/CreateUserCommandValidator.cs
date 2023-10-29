using FluentValidation;

namespace WebFootballApp.Features.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    // public CreateUserCommandValidator()
    // {
    //     RuleFor(u => u.Name)
    //         .NotEmpty()
    //         .MaximumLength(50);
    // }
}