using CrmCore.Core.Application.Task.DTO;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Core.Application.Task.Queries.GetTaskById;

public static class TaskMapper
{
    public static TaskDTO ToDTO(TaskAggregate task, List<UserAggregate> users)
    {
        UserAggregate executor = users.First(u => u.Id == task.Executor.UserId);
        UserAggregate director = users.First(u => u.Id == task.Director.UserId);

        List<TaskUserDTO> coExecutors = task.CoExecutors.Select(c =>
        {
            var user = users.First(u => u.Id == c.UserId);
            return new TaskUserDTO(
                user.Id,
                user.Name.First,
                user.Name.Last,
                user.Name.Middle
            );
        }).ToList();

        List<TaskUserDTO> observers = task.Observers.Select(o =>
        {
            var user = users.First(u => u.Id == o.UserId);
            return new TaskUserDTO(
                user.Id,
                user.Name.First,
                user.Name.Last,
                user.Name.Middle
            );
        }).ToList();

        List<TaskCommentDTO> comments = task.Comments.Select(c => new TaskCommentDTO(
            c.Id,
            c.UserId,
            c.Value,
            c.Date
        )).ToList();

        return new TaskDTO(
            task.Id,
            task.Title,
            task.Description,
            task.DeadLine,
            task.CreatedAt,
            task.Status,
            new TaskUserDTO(executor.Id, executor.Name.First, executor.Name.Last, executor.Name.Middle),
            new TaskUserDTO(director.Id, director.Name.First, director.Name.Last, director.Name.Middle),
            coExecutors,
            observers,
            comments
        );
    }
}