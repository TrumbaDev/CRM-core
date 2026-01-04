namespace CrmCore.Tests.Application.User.Queries;

using CrmCore.Application.User.DTO;
using CrmCore.Application.User.Queries.GetUserById;
using CrmCore.Domain.User.Aggregate;
using CrmCore.Domain.User.Repositories;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetUserByIdQueryHandlerTests
{
    private Mock<IUserRepository> _repoMock = null!;
    private GetUserByIdQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IUserRepository>();
        _handler = new GetUserByIdQueryHandler(_repoMock.Object);
    }

    [Test]
    public async Task Should_Return_UserDTO_When_Found()
    {
        var user = new User(new FullName("John", "Doe"), new Email("john@doe.com"), new PhoneNumber("123"));
        _repoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        var dto = await _handler.Handle(new GetUserByIdQuery(1));

        Assert.That(dto, Is.Not.Null);
        Assert.That(dto!.Email, Is.EqualTo("john@doe.com"));
    }

    [Test]
    public async Task Should_Return_Null_When_NotFound()
    {
        _repoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User?)null);

        var dto = await _handler.Handle(new GetUserByIdQuery(1));

        Assert.That(dto, Is.Null);
    }
}
