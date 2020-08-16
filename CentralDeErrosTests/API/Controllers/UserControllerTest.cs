using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.Controllers;
using CentralDeErros.RequestValidations;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.API.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void shoud_get_user_by_id()
        {
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            var user = new User()
            {
                Name = "teste",
                Email = "teste4@teste.com",
            };

            userServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            var userController = new UserController(userServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.GetById(1);

            var result = expectedUserList.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void shoud_get_all_users()
        {
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            var userList = new List<User>()
            {
                new User()
                {
                    Name = "teste",
                    Email = "teste4@teste.com",
                },
                new User()
                {
                    Name = "teste",
                    Email = "teste3@teste.com",
                },
            };

            userServiceMock.Setup(x => x.GetAll()).Returns(userList);

            var userController = new UserController(userServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.GetAll();

            var result = expectedUserList.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void shoud_update_users()
        {
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            var user = new UpdateUserRequestValidation()
            {
                Id = 1,
                Name = "teste",
                Email = "teste4@teste.com",
            };

            userServiceMock.Setup(x => x.Update(It.IsAny<User>()));

            var userController = new UserController(userServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.Update(user);

            Assert.NotNull(expectedUserList);
        }

        [Fact]
        public void shoud_post_users()
        {
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            var user = new CreateUserRequestValidation()
            {
                Name = "teste",
                Email = "teste4@teste.com",
                Password = "senha"
            };

            var userId = 1;

            userServiceMock.Setup(x => x.Save(It.IsAny<User>())).Returns(userId);

            var userController = new UserController(userServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.Post(user);

            var result = expectedUserList.Result as OkObjectResult;

            Assert.NotNull(expectedUserList);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(userId, result.Value);
        }

        [Fact]
        public void shoud_login_users()
        {
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            var user = new LoginUserRequestValidation()
            {
                Email = "teste4@teste.com",
                Password = "senha"
            };

            var tokenData = new {
                user = new User()
                {
                    Name = "teste",
                    Email = "teste4@teste.com",
                },
                token = "userToken"
            };

            userServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(tokenData);

            var userController = new UserController(userServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.Login(user);

            var result = expectedUserList.Result as OkObjectResult;

            Assert.NotNull(expectedUserList);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(tokenData, result.Value);
        }
    }
}
