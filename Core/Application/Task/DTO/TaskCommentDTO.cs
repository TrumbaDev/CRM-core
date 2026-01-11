namespace CrmCore.Core.Application.Task.DTO;

public record TaskCommentDTO(
    int Id,
    int AuthorId,
    string Text,
    DateTime CreatedAt
);