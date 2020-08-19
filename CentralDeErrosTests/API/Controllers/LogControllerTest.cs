using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.Controllers;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.API.Controllers
{
    public class LogControllerTest
    {
        [Fact]
        public void shoud_get_log_by_id()
        {
            var logServiceMock = new Mock<ILogService>();
            var mapperMock = new Mock<IMapper>();

            var log = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            logServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(log);

            var userController = new LogController(logServiceMock.Object, mapperMock.Object);

            var expectedUserList = userController.GetById(1);

            var result = expectedUserList.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

    }
}
