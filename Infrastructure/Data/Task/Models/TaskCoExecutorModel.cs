using CrmCore.Infrastructure.Data.User.Models;

namespace CrmCore.Infrastructure.Data.Task.Models;

public class TaskCoExecutorModel
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    public TaskModel Task { get; set; } = null!;

    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}