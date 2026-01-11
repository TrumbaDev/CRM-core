
namespace CrmCore.Core.Domain.Task.Aggregate;

public class Executor
{
    public int UserId { get; private set; }

    private Executor() { }

    public static Executor Create(int userId)
    {
        return new Executor
        {
            UserId = userId
        };
    }
}