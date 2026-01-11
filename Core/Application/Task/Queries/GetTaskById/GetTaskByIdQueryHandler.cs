using CrmCore.Core.Application.Task.DTO;
using CrmCore.Core.Domain.Task.Repositories;
using CrmCore.Core.Domain.User.Repositories;
using MediatR;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Core.Application.Task.Queries.GetTaskById;

public class GetTaskByIdQueryHandler: IRequestHandler<GetTaskByIdQuery, TaskDTO?>
{
    private readonly ITaskRepository _taskRepo;
    private readonly IUserRepository _userRepo;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepo, IUserRepository userRepo)
    {
        _taskRepo = taskRepo;
        _userRepo = userRepo;
    }

    public async Task<TaskDTO?> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await _taskRepo.GetByIdAsync(query.Id);
        if(task is null) return null;

        List<int> userIds = task.CoExecutors.Select(c => c.UserId)
                .Concat(task.Observers.Select(o => o.UserId))
                .Append(task.Executor.UserId)
                .Append(task.Director.UserId)
                .Distinct()
                .ToList();
        
        List<UserAggregate> users = await _userRepo.GetByIdsAsync(userIds);
        if(users.Count == 0) return null;

       return TaskMapper.ToDTO(task, users);
    }
}