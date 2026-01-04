using CrmCore.Application.Common.Services;
using CrmCore.Domain.User.Aggregate;
using CrmCore.Domain.User.Repositories;
using UserAggregate = CrmCore.Domain.User.Aggregate.User;

namespace CrmCore.Application.User.Commands.CreateUser;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _repo;

    public CreateUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateUserCommand cmd)
    {
        string formatedPhone = PhoneFormatter.Format(cmd.Phone);
        var existsUser = await _repo.GetByEmailOrPhoneAsync(
            cmd.Email,
            formatedPhone
        );
        if (existsUser is not null)
            throw new InvalidOperationException("User with this email or phone already exists");

        var user = UserAggregate.Create(
            new FullName(cmd.First, cmd.Last, cmd.Middle),
            new Email(cmd.Email),
            new PhoneNumber(formatedPhone)
        );

        return await _repo.AddAsync(user);
    }
}