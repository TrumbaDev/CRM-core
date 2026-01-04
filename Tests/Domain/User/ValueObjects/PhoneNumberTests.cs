namespace CrmCore.Test.Domain.User.ValueObjects;

using CrmCore.Domain.User.Aggregate;
using NUnit.Framework;

[TestFixture]
public class PhoneNumberTests
{
    [Test]
    public void Should_Create_PhoneNumber_When_Valid()
    {
        var phone = new PhoneNumber("1234567890");
        Assert.That(phone.Value, Is.EqualTo("1234567890"));
    }

    [Test]
    public void Should_Throw_When_Phone_Is_Null_Or_Whitespace()
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(""));
        Assert.Throws<ArgumentException>(() => new PhoneNumber("   "));
        Assert.Throws<ArgumentException>(() => new PhoneNumber(null!));
    }

    [TestCase("+7 999-123-45-67", " +7 999-123-45-67 ")]
    [TestCase("1234567890", "1234567890")]
    public void Value_Is_Set_Correctly(string expected, string input)
    {
        var phone = new PhoneNumber(input);
        Assert.That(phone.Value, Is.EqualTo(input));
    }
}
