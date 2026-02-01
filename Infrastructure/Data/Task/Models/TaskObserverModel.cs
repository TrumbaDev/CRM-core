
namespace CrmCore.Infrastructure.Data.Task.Models;

public class TaskObserverModel
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}