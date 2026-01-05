using CrmCore.Core.Domain.User.Aggregate;
using CrmCore.Core.Domain.User.Repositories;
using CrmCore.Infrastructure.Data.User.Models;
using Microsoft.EntityFrameworkCore;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Infrastructure.Data.User.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<UserAggregate?> GetByIdAsync(int id)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (model is null) return null;

        return new UserAggregate(
            model.Id,
            new FullName(model.FirstName, model.LastName, model.MiddleName),
            new Email(model.Email),
            new PhoneNumber(model.Phone)
        );
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

        if(model is null) return null;

        return new UserAggregate(
            model.Id,
            new FullName(model.FirstName, model.LastName, model.MiddleName),
            new Email(model.Email),
            new PhoneNumber(model.Phone)
        );
    }
}