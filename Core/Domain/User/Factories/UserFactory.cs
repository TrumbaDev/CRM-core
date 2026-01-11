using CrmCore.Core.Domain.User.Aggregate;
using CrmCore.Infrastructure.Data.User.Models;
using UserAggregate = CrmCore.Core.Domain.User.Aggregate.User;

namespace CrmCore.Core.Domain.User.Factories;

public class UserFactory
{
    public UserAggregate Rehydrate(UserModel model)
    {
        return new UserAggregate(
            model.Id,
            new FullName(model.FirstName, model.LastName, model.MiddleName),
            new Email(model.Email),
            new PhoneNumber(model.Phone)
        );
    }

    public List<UserAggregate> Rehydrate(List<UserModel> models)
    {
        return models.Select(Rehydrate).ToList();
    }
}