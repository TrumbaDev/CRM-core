using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Infrastructure.Data.Task.Models;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Core.Domain.Task.Factories;

public class TaskAggregateFactory
{
    public TaskAggregate Rehydrate(TaskModel model)
    {
        List<CoExecutor> coExecutors = model.CoExecutors
           .Select(c => new CoExecutor(
                   c.UserId
               )
           ).ToList();

        List<Observer> observers = model.Observers
            .Select(o => new Observer(
                    o.UserId
                )
            ).ToList();

        List<Comment> comments = model.Comments
            .Select(c => new Comment(
                    c.Id,
                    c.UserId,
                    c.Value,
                    c.UpdatedAt
                )
            ).ToList();

        return TaskAggregate.Rehydrate(
            model.Id,
            model.Title,
            model.Description,
            model.DeadLine,
            model.CreatedAt,
            model.Status,
            Executor.Create(model.ExecutorId),
            Director.Create(model.DirectorId),
            coExecutors,
            observers,
            comments
        );
    }
}