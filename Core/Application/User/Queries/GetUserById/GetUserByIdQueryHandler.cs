using CrmCore.Core.Application.User.DTO;
using CrmCore.Core.Domain.User.Repositories;
using MediatR;

namespace CrmCore.Core.Application.User.Queries.GetUserById;

public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserDTO?>
{
    private readonly IUserRepository _repo;

    public GetUserByIdQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserDTO?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _repo.GetByIdAsync(query.Id);
        if(user is null) return null;

        return new UserDTO(
            user.Id,
            user.Name.First,
            user.Name.Last,
            user.Name.Middle,
            user.Email.Value,
            user.Phone.Value
        );
    }
}