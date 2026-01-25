using CrmCore.Core.Domain.Common.Exceptions;
using CrmCore.Core.Domain.Task.Aggregate;
using CrmCore.Core.Domain.Task.Repositories;
using MediatR;
using TaskAggregate = CrmCore.Core.Domain.Task.Aggregate.Task;

namespace CrmCore.Core.Application.Task.Commands.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, int>
{
    private readonly ITaskRepository _taskRepo;

    public AddCommentCommandHandler(ITaskRepository taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<int> Handle(AddCommentCommand cmd, CancellationToken cancellationToken)
    {
        if (cmd.Comment == "")
            throw new ArgumentException("Blank comment");

        if (cmd.UserId <= 0)
            throw new ArgumentException("UserId is required");

        if (cmd.TaskId <= 0)
            throw new ArgumentException("TaskId is required");

        TaskAggregate task = await _taskRepo.GetByIdAsync(cmd.TaskId)
            ?? throw new DomainException("Undefined task");

        task.AddComment(
            Comment.Create(
                cmd.UserId,
                cmd.Comment
            )
        );

        return await _taskRepo.AddCommentAsync(task);
    }
}