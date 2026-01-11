using CrmCore.Core.Application.Task.DTO;
using CrmCore.Core.Domain.Task.Aggregate;
using MediatR;

namespace CrmCore.Core.Application.Task.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string Description,
    DateTime DeadLine,
    DateTime CreatedAt,
    Status Status,
    TaskUserDTO Executor,
    TaskUserDTO Director,
    IReadOnlyCollection<TaskUserDTO> CoExecutors,
    IReadOnlyCollection<TaskUserDTO> Observers
): IRequest<int>;