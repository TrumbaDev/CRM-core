using CrmCore.Infrastructure.Data.User.Models;

namespace CrmCore.Core.Domain.Task.Aggregate;

public class Director
{
    public int UserId { get; private set;}
    public FullName Name { get; private set; } = null!;

    private Director() { }

    public static Director FromUserModel(UserModel user)
    {
        return new Director
        {
            UserId = user.Id,
            Name  = new FullName(user.FirstName, user.LastName, user.MiddleName)
        };
    }

    public static Director Create(string firstName, string lastName, string middleName)
    {
        return new Director
        {
            Name = new FullName(firstName, lastName, middleName)
        };
    }
}