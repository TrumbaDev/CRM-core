namespace CrmCore.Test.Domain.User.ValueObjects;

using CrmCore.Domain.User.Aggregate;
using NUnit.Framework;

[TestFixture]
public class EmailTests
{
    [Test]
    public void Should_Create_Email_When_Valid()
    {
        var email = new Email("test@example.com");
        Assert.That(email.Value, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void Should_Throw_When_Invalid_Email()
    {
        Assert.Throws<ArgumentException>(() => new Email(""));
        Assert.Throws<ArgumentException>(() => new Email("invalidemail"));
        Assert.Throws<ArgumentException>(() => new Email("   "));
    }
}
