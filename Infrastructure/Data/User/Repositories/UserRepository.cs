using CrmCore.Core.Domain.User.Factories;
using CrmCore.Core.Domain.User.Repositories;
using CrmCore.Infrastructure.Data.User.Models;
using Microsoft.EntityFrameworkCore;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Infrastructure.Data.User.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly UserFactory _factory;

    public UserRepository(UserDbContext context, UserFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task<UserAggregate?> GetByIdAsync(int id)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return model is null ? null : _factory.Rehydrate(model);
    }

    public async Task<List<UserAggregate>> GetByIdsAsync(List<int> userIds)
    {
        List<UserModel> models = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

       return _factory.Rehydrate(models);
    }

    public async Task<int> AddAsync(UserAggregate user)
    {
        UserModel model = new()
        {
            FirstName = user.Name.First,
            LastName = user.Name.Last,
            MiddleName = user.Name.Middle,
            Email = user.Email.Value,
            Phone = user.Phone.Value
        };

        _context.Users.Add(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<UserAggregate?> GetByEmailOrPhoneAsync(string email, string phone)
    {
        var model = await _context.Users.FirstOrDefaultAsync(
            x => x.Email == email || x.Phone == phone
        );

       return model is null ? null : _factory.Rehydrate(model);
    }
}