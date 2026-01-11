using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Core.Domain.Task.Repositories;
using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Infrastructure.Data.Task.Repositories;

public class TaskRepositroy : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepositroy(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskAggregate?> GetByIdAsync(int id)
    {
        var model = await _context.Task
            .Include(t => t.Executor)
            .Include(t => t.Director)
            .Include(t => t.CoExecutors)
            .Include(t => t.Observers)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (model is null) return null;

        List<CoExecutor> coExecutors = model.CoExecutors
            .Select(c => new CoExecutor(
                    c.UserId,
                    new FullName(c.User.FirstName, c.User.LastName, c.User.MiddleName)
                )
            ).ToList();

        List<Observer> observers = model.Observers
            .Select(o => new Observer(
                    o.UserId,
                    new FullName(o.User.FirstName, o.User.LastName, o.User.MiddleName)
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
            Executor.FromUserModel(model.Executor),
            Director.FromUserModel(model.Director),
            coExecutors,
            observers,
            comments
        );
    }

    public async Task<int> AddAsync(TaskAggregate task)
    {
        TaskModel model = new()
        {
            Title = task.Title,
            Description = task.Description,
            DeadLine = task.DeadLine,
            Status = task.Status,
            ExecutorId = task.Executor.UserId,
            DirectorId = task.Director.UserId,
        };

        foreach (CoExecutor co in task.CoExecutors)
        {
            model.CoExecutors.Add(new TaskCoExecutorModel
            {
                UserId = co.UserId
            });
        }

        foreach (Observer obs in task.Observers)
        {
            model.Observers.Add(new TaskObserverModel
            {
                UserId = obs.UserId
            });
        }

        foreach (Comment comment in task.Comments)
        {
            model.Comments.Add(new TaskCommentModel
            {
                UserId = comment.UserId,
                Value = comment.Value
            });
        }

        _context.Task.Add(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }
}