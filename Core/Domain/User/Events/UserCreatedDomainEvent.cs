using CrmCore.Domain.Common;

namespace CrmCore.Domain.User.Events;

public record UserCreatedDomainEvent(int UserId) : IDomainEvent;