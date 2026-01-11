using CrmCore.Core.Domain.Task.Aggregate;

namespace CrmCore.Infrastructure.Data.Task.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DeadLine { get; set; }
    public Status Status { get; set; }

    public int ExecutorId { get; set; }

    public int DirectorId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<TaskCoExecutorModel> CoExecutors { get; set; } = [];
    public ICollection<TaskObserverModel> Observers { get; set; } = [];
    public ICollection<TaskCommentModel> Comments { get; set; } = [];
}