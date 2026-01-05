using CrmCore.Core.Domain.Common;
using CrmCore.Core.Domain.User.Events;

namespace CrmCore.Core.Domain.User.Aggregate;

public class User
{
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    public int Id { get; private set; }
    public FullName Name { get; internal set; } = null!;
    public Email Email { get; internal set; } = null!;
    public PhoneNumber Phone { get; internal set; } = null!;

    private User() { }

    internal User(FullName name, Email email, PhoneNumber phone)
    {
        Name = name;
        Email = email;
        Phone = phone;

        AddEvent(new UserCreatedDomainEvent(Id));
    }

    internal User(int id, FullName name, Email email, PhoneNumber phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }


    public static User Create(FullName name, Email email, PhoneNumber phone)
    {
        return new User(name, email, phone);
    }

    private void AddEvent(IDomainEvent @event) => _events.Add(@event);
}