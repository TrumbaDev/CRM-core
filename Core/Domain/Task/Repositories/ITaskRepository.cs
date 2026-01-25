using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Core.Domain.Task.Repositories;

public interface ITaskRepository
{
    Task<TaskAggregate?> GetByIdAsync(int id);
    Task<int> AddAsync(TaskAggregate taks);
    Task<int> AddCommentAsync(TaskAggregate task);
}