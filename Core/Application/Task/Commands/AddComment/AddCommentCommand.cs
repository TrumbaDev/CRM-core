using MediatR;

namespace CrmCore.Core.Application.Task.Commands.AddComment;

public record AddCommentCommand(
    int TaskId,
    int UserId,
    string Comment
): IRequest<int>;