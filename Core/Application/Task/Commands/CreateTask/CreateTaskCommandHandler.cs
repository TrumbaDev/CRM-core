
using CrmCore.Core.Application.Task.DTO;
using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Core.Domain.Task.Repositories;
using MediatR;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Core.Application.Task.Commands.CreateTask;

public class CreateTaskCommandHandler: IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _repo;

    public CreateTaskCommandHandler(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateTaskCommand cmd, CancellationToken cancellationToken)
    {
        Executor executor = Executor.Create(
            cmd.Executor.UserId
        );
        Director director = Director.Create(
            cmd.Director.UserId
        );

        TaskAggregate task = TaskAggregate.Create(
            cmd.Title,
            cmd.Description,
            cmd.DeadLine,
            executor,
            director
        );

        foreach (TaskUserDTO coExecutor in cmd.CoExecutors)
        {
            task.AddCoExecutor(CoExecutor.Create(
                coExecutor.UserId
            ));
        }

        foreach (TaskUserDTO observer in cmd.Observers)
        {
            task.AddObserver(Observer.Create(
                observer.UserId
            ));
        }

        return await _repo.AddAsync(task);
    }
}