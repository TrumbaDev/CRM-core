
using CrmCore.Core.Application.Task.DTO;
using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Core.Domain.Task.Repositories;
using CrmCore.Core.Domain.User.Repositories;
using MediatR;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Core.Application.Task.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _taskRepo;
    private readonly IUserRepository _userRepo;

    public CreateTaskCommandHandler(ITaskRepository taskRepo, IUserRepository userRepo)
    {
        _taskRepo = taskRepo;
        _userRepo = userRepo;
    }

    public async Task<int> Handle(CreateTaskCommand cmd, CancellationToken cancellationToken)
    {
        if (await UsersCount(cmd) == 0)
            throw new ArgumentException("Invalid peecked users");

        Executor executor = Executor.Create(
            cmd.Executor.UserId
        );
        Director director = Director.Create(
            cmd.Director.UserId
        );

        TaskAggregate task = TaskAggregate.Create(
            cmd.Title,
            cmd.Description ?? "",
            cmd.DeadLine,
            executor,
            director
        );

        foreach (TaskUserDTO coExecutor in cmd.CoExecutors ?? [])
        {
            task.AddCoExecutor(CoExecutor.Create(
                coExecutor.UserId
            ));
        }

        foreach (TaskUserDTO observer in cmd.Observers ?? [])
        {
            task.AddObserver(Observer.Create(
                observer.UserId
            ));
        }

        return await _taskRepo.AddAsync(task);
    }

    private async Task<int> UsersCount(CreateTaskCommand cmd)
    {
        List<int> userIds =
        [
            cmd.Executor.UserId,
            cmd.Director.UserId
        ];

        if (cmd.CoExecutors is not null)
            userIds.AddRange(cmd.CoExecutors.Select(x => x.UserId));

        if (cmd.Observers is not null)
            userIds.AddRange(cmd.Observers.Select(x => x.UserId));

        userIds = userIds.Distinct().ToList();

        List<UserAggregate> users = await _userRepo.GetByIdsAsync(userIds);
        return users.Count;
    }
}