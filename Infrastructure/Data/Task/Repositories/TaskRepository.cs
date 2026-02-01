using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Core.Domain.Task.Factories;
using CrmCore.Core.Domain.Task.Repositories;
using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Infrastructure.Data.Task.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;
    private readonly TaskAggregateFactory _factory;

    public TaskRepository(TaskDbContext context, TaskAggregateFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task<TaskAggregate?> GetByIdAsync(int id)
    {
        TaskModel? model = await _context.Task
            .Include(t => t.CoExecutors)
            .Include(t => t.Observers)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);

        return model is null ? null : _factory.Rehydrate(model);
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

        _context.Task.Add(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<int> AddCommentAsync(TaskAggregate task)
    {
        TaskModel model = await _context.Task
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == task.Id)
            ?? throw new RepositoryException("Task not found");

        Comment lastComment = task.Comments.Last();
        model.Comments.Add(new TaskCommentModel
        {
            TaskId = task.Id,
            UserId = lastComment.UserId,
            Value = lastComment.Value
        });
        await _context.SaveChangesAsync();
        return model.Comments.Last().Id;
    }
}