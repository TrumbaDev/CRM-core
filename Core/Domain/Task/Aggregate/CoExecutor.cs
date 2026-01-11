namespace CrmCore.Core.Domain.Task.Aggregate;

public class CoExecutor
{
    public int UserId { get; private set; }

    private CoExecutor() { }

    internal CoExecutor(int userId)
    {
        UserId = userId;
    }

    public static CoExecutor Create(int userId)
    {
        return new CoExecutor
        {
            UserId = userId
        };
    }
}