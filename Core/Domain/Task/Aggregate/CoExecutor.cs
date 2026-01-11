namespace CrmCore.Core.Domain.Task.Aggregate;

public class CoExecutor
{
    public int UserId { get; private set; }
    public FullName Name { get; private set; } = null!;

    private CoExecutor() { }

    internal CoExecutor(int userId, FullName name)
    {
        UserId = userId;
        Name = name;
    }

    public static CoExecutor Create(string firstName, string lastName, string middleName)
    {
        return new CoExecutor
        {
            Name = new FullName(firstName, lastName, middleName)
        };
    }
}