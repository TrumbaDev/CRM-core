namespace CrmCore.Tests.Application.Common;

using CrmCore.Application.Common.Services;
using NUnit.Framework;

[TestFixture]
public class PhoneFormatterTests
{
    [TestCase(" +7 999-123-45-67 ", "79991234567")]
    [TestCase("(123) 456-7890", "1234567890")]
    public void Should_Format_Phone_Correctly(string raw, string expected)
    {
        var formatted = PhoneFormatter.Format(raw);
        Assert.That(formatted, Is.EqualTo(expected));
    }
}
