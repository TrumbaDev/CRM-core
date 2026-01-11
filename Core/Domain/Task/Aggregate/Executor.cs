using CrmCore.Infrastructure.Data.User.Models;

namespace CrmCore.Core.Domain.Task.Aggregate;

public class Executor
{
    public int UserId { get; private set; }
    public FullName Name { get; private set; } = null!;

    private Executor() { }

    public static Executor FromUserModel(UserModel user)
    {
        return new Executor
        {
            UserId = user.Id,
            Name = new FullName(user.FirstName, user.LastName, user.MiddleName)
        };
    }

    public static Executor Create(string firstName, string lastName, string middleName)
    {
        return new Executor
        {
            Name = new FullName(firstName, lastName, middleName)
        };
    }
}