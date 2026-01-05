namespace CrmCore.Core.Application.User.Commands.CreateUser;

public record CreateUserCommand(
    string First,
    string Last,
    string Middle,
    string Email,
    string Phone
);