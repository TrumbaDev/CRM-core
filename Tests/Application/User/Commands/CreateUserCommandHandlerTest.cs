namespace CrmCore.Tests.Application.User.Commands;

using CrmCore.Core.Application.User.Commands.CreateUser;
using CrmCore.Core.Domain.User.Aggregate;
using CrmCore.Core.Domain.User.Repositories;
using Moq;
using NUnit.Framework;

[TestFixture]
public class CreateUserCommandHandlerTests
{
    private Mock<IUserRepository> _repoMock = null!;
    private CreateUserCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IUserRepository>();
        _handler = new CreateUserCommandHandler(_repoMock.Object);
    }

    [Test]
    public async Task Should_Throw_If_User_Exists()
    {
        _repoMock.Setup(x => x.GetByEmailOrPhoneAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new User(new FullName("A", "B"), new Email("a@b.com"), new PhoneNumber("123")));

        var cmd = new CreateUserCommand("A", "B", "", "a@b.com", "123");

        InvalidOperationException? ex = null;

        try
        {
            await _handler.Handle(cmd);
        }
        catch (InvalidOperationException e)
        {
            ex = e;
        }

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("User with this email or phone already exists"));
    }

    [Test]
    public async Task Should_Create_User_And_Return_Id()
    {
        _repoMock.Setup(x => x.GetByEmailOrPhoneAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _repoMock.Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(42);

        var cmd = new CreateUserCommand("John", "Doe", "", "john@doe.com", "+7 999-123-45-67");

        var id = await _handler.Handle(cmd);

        Assert.That(id, Is.EqualTo(42));
        _repoMock.Verify(x => x.AddAsync(It.Is<User>(u => u.Email.Value == "john@doe.com")), Times.Once);
    }
}
