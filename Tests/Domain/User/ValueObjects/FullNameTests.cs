namespace CrmCore.Test.Domain.User.ValueObjects;

using CrmCore.Core.Domain.User.Aggregate;
using NUnit.Framework;

[TestFixture]
public class FullNameTests
{
    [Test]
    public void Should_Create_FullName_When_Valid()
    {
        var name = new FullName("John", "Doe", "Middle");
        Assert.That(name.First, Is.EqualTo("John"));
        Assert.That(name.Last, Is.EqualTo("Doe"));
        Assert.That(name.Middle, Is.EqualTo("Middle"));
    }

    [Test]
    public void Should_Throw_When_FirstOrLast_Empty()
    {
        Assert.Throws<ArgumentException>(() => new FullName("", "Doe"));
        Assert.Throws<ArgumentException>(() => new FullName("John", ""));
    }
}
