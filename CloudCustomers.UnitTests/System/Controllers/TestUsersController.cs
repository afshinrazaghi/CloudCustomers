using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.System.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        // Arrange 
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UserFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);

        // Act
        var res = (OkObjectResult)await sut.Get();

        // Assert
        res.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserService()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var res = await sut.Get();

        // Assert
        mockUsersService.Verify(
            service => service.GetAllUsers(),
            Times.Once()
        );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(UserFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);

        // Act
        var res = await sut.Get();

        // Assert
        res.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)res;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}