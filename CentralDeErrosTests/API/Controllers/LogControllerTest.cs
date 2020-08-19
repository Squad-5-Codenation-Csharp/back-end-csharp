using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.Controllers;
using CentralDeErros.RequestValidations;
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


        [Fact]
        public void shoud_get_all_logs()
        {
            var logServiceMock = new Mock<ILogService>();
            var mapperMock = new Mock<IMapper>();

            var logList = new List<Log>()
            {
                new Log()
                {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
                },
                new Log()
                {
                Id = 2,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Dev",
                UserId = 2
                }
            };

            logServiceMock.Setup(x => x.GetAll(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>())).Returns(logList);

            var logController = new LogController(logServiceMock.Object, mapperMock.Object);

            var expectedLogList = logController.GetAll(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>());

            var result = expectedLogList.Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public void shoud_post_log()
        {
            var logServiceMock = new Mock<ILogService>();
            var mapperMock = new Mock<IMapper>();

            var log  = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var logToBeCreated = new CreateLogRequestValidation()
            {
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var logId = 1;

            logServiceMock.Setup(x => x.Save(log)).Returns(logId);

            var logController = new LogController(logServiceMock.Object, mapperMock.Object);

            var posted = logController.Post(logToBeCreated);

            var result = posted.Result as OkObjectResult;

            Assert.NotNull(posted);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(logId, result.Value);
        }
    }
}
