
namespace CrmCore.Infrastructure.Data.Task.Models;

public class TaskCoExecutorModel
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}