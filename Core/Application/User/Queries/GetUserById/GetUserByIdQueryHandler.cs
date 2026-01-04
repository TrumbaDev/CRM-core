using CrmCore.Application.User.DTO;
using CrmCore.Domain.User.Repositories;

namespace CrmCore.Application.User.Queries.GetUserById;

public class GetUserByIdQueryHandler
{
    private readonly IUserRepository _repo;

    public GetUserByIdQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserDTO?> Handle(GetUserByIdQuery query)
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