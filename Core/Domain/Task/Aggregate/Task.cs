using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.Common.Exceptions;

namespace CrmCore.Core.Domain.Task.Aggregate;

public class Task
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime DeadLine { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Status Status { get; private set; }
    public Executor Executor { get; private set; } = null!;
    public Director Director { get; private set; } = null!;

    private readonly List<CoExecutor> _coExecutors = [];
    public IReadOnlyCollection<CoExecutor> CoExecutors => _coExecutors.AsReadOnly();

    private readonly List<Observer> _observers = [];
    public IReadOnlyCollection<Observer> Observers => _observers.AsReadOnly();

    private readonly List<Comment> _comments = [];
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private Task() { }

    public static Task Create(
        string title,
        string description,
        DateTime deadLine,
        Executor executor,
        Director director
    )
    {
        return new Task
        {
            Title = title,
            Description = description,
            DeadLine = deadLine,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Created,
            Executor = executor,
            Director = director
        };
    }

    internal static Task Rehydrate(
        int id,
        string title,
        string description,
        DateTime deadLine,
        DateTime createdAt,
        Status status,
        Executor executor,
        Director director,
        IEnumerable<CoExecutor> coExecutors,
        IEnumerable<Observer> observers,
        IEnumerable<Comment> comments
    )
    {
        Task task = new()
        {
            Id = id,
            Title = title,
            Description = description,
            DeadLine = deadLine,
            CreatedAt = createdAt,
            Status = status,
            Executor = executor,
            Director = director
        };

        task._coExecutors.AddRange(coExecutors);
        task._observers.AddRange(observers);
        task._comments.AddRange(comments);

        return task;
    }

    #region CO-EXECUTOR
    public void AddCoExecutor(CoExecutor coExecutor)
    {
        if (_coExecutors.Any(x => x.UserId == coExecutor.UserId))
            throw new DomainException("User already co-executor");

        if (Executor.UserId == coExecutor.UserId)
            throw new DomainException("Executor cannot be co-executor");

        _coExecutors.Add(coExecutor);
    }

    public void RemoveCoExecutor(int userId)
    {
        var coExecutor = _coExecutors.FirstOrDefault(x => x.UserId == userId)
            ?? throw new NotFoundException("User is not co-executor");
        _coExecutors.Remove(coExecutor);
    }
    #endregion

    #region OBSERVER
    public void AddObserver(Observer observer)
    {
        if (_observers.Any(x => x.UserId == observer.UserId))
            throw new DomainException("User already observer");

        if (Executor.UserId == observer.UserId)
            throw new DomainException("Executor cannot be observer");

        _observers.Add(observer);
    }

    public void RemoveObserver(int userId)
    {
        var observer = _observers.FirstOrDefault(x => x.UserId == userId)
            ?? throw new NotFoundException("User is not observer");
        _observers.Remove(observer);
    }
    #endregion

    #region COMMENT
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void RemoveComment(int commentId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId)
            ?? throw new NotFoundException("Comment not found");
        _comments.Remove(comment);
    }
    #endregion
}