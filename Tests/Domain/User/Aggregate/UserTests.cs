namespace CrmCore.Test.Domain.User.Aggregate;

using CrmCore.Core.Domain.User.Aggregate;
using CrmCore.Core.Domain.User.Events;
using NUnit.Framework;

[TestFixture]
public class UserTests
{
    [Test]
    public void Should_Create_User_And_Add_Event()
    {
        var name = new FullName("John", "Doe");
        var email = new Email("john@doe.com");
        var phone = new PhoneNumber("123456");

        var user = User.Create(name, email, phone);

        Assert.That(user.Name, Is.EqualTo(name));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Phone, Is.EqualTo(phone));
        Assert.That(user.Events, Has.Exactly(1).InstanceOf<UserCreatedDomainEvent>());
    }
}
