
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
            cmd.Executor.FisrtName,
            cmd.Executor.LastName,
            cmd.Executor.MiddleName
        );
        Director director = Director.Create(
            cmd.Director.FisrtName,
            cmd.Director.LastName,
            cmd.Director.MiddleName
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
                coExecutor.FisrtName,
                coExecutor.LastName,
                coExecutor.MiddleName
            ));
        }

        foreach (TaskUserDTO observer in cmd.Observers)
        {
            task.AddObserver(Observer.Create(
                observer.FisrtName,
                observer.LastName,
                observer.MiddleName
            ));
        }

        return await _repo.AddAsync(task);
    }
}