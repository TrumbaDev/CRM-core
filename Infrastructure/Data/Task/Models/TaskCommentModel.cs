using CrmCore.Infrastructure.Data.User.Models;

namespace CrmCore.Infrastructure.Data.Task.Models;

public class TaskCommentModel
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public TaskModel Task { get; set; } = null!;

    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;

    public string Value { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}