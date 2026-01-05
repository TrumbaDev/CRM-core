using CrmCore.Core.Domain.Common;

namespace CrmCore.Core.Domain.User.Events;

public record UserCreatedDomainEvent(int UserId) : IDomainEvent;