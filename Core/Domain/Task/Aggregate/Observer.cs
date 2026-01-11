namespace CrmCore.Core.Domain.Task.Aggregate;

public class Observer
{
    public int UserId { get; private set; }
    public FullName Name { get; private set; } = null!;

    private Observer() { }

    internal Observer(int userId, FullName name)
    {
        UserId = userId;
        Name = name;
    }

    public static Observer Create(string firstName, string lastName, string middleName)
    {
        return new Observer
        {
            Name = new FullName(firstName, lastName, middleName)
        };
    }
}