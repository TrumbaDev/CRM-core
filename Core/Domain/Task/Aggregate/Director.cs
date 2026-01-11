
namespace CrmCore.Core.Domain.Task.Aggregate;

public class Director
{
    public int UserId { get; private set;}

    private Director() { }

    public static Director Create(int userId)
    {
        return new Director
        {
            UserId = userId
        };
    }
}