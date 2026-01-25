using CrmCore.Core.Application.Common.Services;
using CrmCore.Core.Domain.User.Aggregate;
using CrmCore.Core.Domain.User.Repositories;
using MediatR;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Core.Application.User.Commands.CreateUser;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _repo;

    public CreateUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateUserCommand cmd, CancellationToken cancellationToken)
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