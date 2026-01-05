using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Core.Domain.User.Repositories;

public interface IUserRepository
{
    Task<UserAggregate?> GetByIdAsync(int id);
    Task<int> AddAsync(UserAggregate user);
    Task<UserAggregate?> GetByEmailOrPhoneAsync(string email, string phone);
}