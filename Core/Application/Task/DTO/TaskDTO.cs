using CrmCore.Core.Domain.Task.Aggregate;

namespace CrmCore.Core.Application.Task.DTO;

public record TaskDTO(
    int Id,
    string Title,
    string Description,
    DateTime DeadLine,
    DateTime CreatedAt,
    Status Status,
    TaskUserDTO Executor,
    TaskUserDTO Director,
    IReadOnlyCollection<TaskUserDTO> CoExecutors,
    IReadOnlyCollection<TaskUserDTO> Observers,
    IReadOnlyCollection<TaskCommentDTO> Comments
);