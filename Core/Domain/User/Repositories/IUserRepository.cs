using UserAggregate = CrmCore.Domain.User.Aggregate.User;

namespace CrmCore.Domain.User.Repositories;

public interface IUserRepository
{
    Task<UserAggregate?> GetByIdAsync(int id);
    Task<int> AddAsync(UserAggregate user);
    Task<UserAggregate?> GetByEmailOrPhoneAsync(string email, string phone);
}