namespace CrmCore.Core.Domain.Task.Aggregate;

public class Observer
{
    public int UserId { get; private set; }

    private Observer() { }

    internal Observer(int userId)
    {
        UserId = userId;
    }

    public static Observer Create(int userId)
    {
        return new Observer
        {
            UserId = userId
        };
    }
}