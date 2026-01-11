using CrmCore.Core.Application.Task.DTO;
using CrmCore.Core.Domain.Task.Repositories;
using MediatR;

namespace CrmCore.Core.Application.Task.Queries.GetTaskById;

public class GetTaskByIdQueryHandler: IRequestHandler<GetTaskByIdQuery, TaskDTO?>
{
    private readonly ITaskRepository _repo;

    public GetTaskByIdQueryHandler(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<TaskDTO?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await _repo.GetByIdAsync(query.Id);
        if(task is null) return null;

        TaskUserDTO executor = new (
            task.Executor.UserId,
            task.Executor.Name.First,
            task.Executor.Name.Last,
            task.Executor.Name.Middle
        );

        TaskUserDTO director = new (
            task.Director.UserId,
            task.Director.Name.First,
            task.Director.Name.Last,
            task.Director.Name.Middle
        );

        IReadOnlyCollection<TaskUserDTO> coExecutors = task.CoExecutors
            .Select(x => new TaskUserDTO(
                x.UserId,
                x.Name.First,
                x.Name.Last,
                x.Name.Middle
            )).ToList();

        IReadOnlyCollection<TaskUserDTO> observers = task.Observers
            .Select(x => new TaskUserDTO(
                x.UserId,
                x.Name.First,
                x.Name.Last,
                x.Name.Middle
            )).ToList();

        IReadOnlyCollection<TaskCommentDTO> comments = task.Comments
            .Select(x => new TaskCommentDTO(
                x.Id,
                x.UserId,
                x.Value,
                x.Date
            )).ToList();

        return new TaskDTO(
            task.Id,
            task.Title,
            task.Description,
            task.DeadLine,
            task.CreatedAt,
            task.Status,
            executor,
            director,
            coExecutors,
            observers,
            comments
        );
    }
}