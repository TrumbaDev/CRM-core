namespace CrmCore.Core.Domain.Task.Aggregate;

public class Comment
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }

    private Comment() { }

    internal Comment(int id, int userId, string value, DateTime date)
    {
        Id = id;
        UserId = userId;
        Value = value;
        Date = date;
    }
}