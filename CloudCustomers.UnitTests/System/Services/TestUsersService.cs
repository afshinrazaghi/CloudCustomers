﻿using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.System.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            var res = await sut.GetAllUsers();

            // Assert
            handlerMock
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(request => request.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            var res = await sut.GetAllUsers();

            // Assert
            res.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var endpoint = "https://example.com/users";
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            var res = await sut.GetAllUsers();

            // Assert
            res.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            var res = await sut.GetAllUsers();

            // Assert
            res.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);
            // Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(request => request.Method == HttpMethod.Get && request.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
