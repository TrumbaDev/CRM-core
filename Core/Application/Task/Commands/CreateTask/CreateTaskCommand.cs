using CrmCore.Core.Application.Task.DTO;
using MediatR;

namespace CrmCore.Core.Application.Task.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    DateTime DeadLine,
    TaskUserDTO Executor,
    TaskUserDTO Director,
    IReadOnlyCollection<TaskUserDTO>? CoExecutors,
    IReadOnlyCollection<TaskUserDTO>? Observers
): IRequest<int>;