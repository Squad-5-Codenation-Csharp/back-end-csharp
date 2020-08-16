using CentralDeErros.Api.Models;
using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.Business.Services
{
    public class LogServiceTest
    {
        [Fact]
        public void shoud_get_all_logs()
        {
            var repositoryMock = new Mock<ILogRepository>();

            var logList = new List<Log>()
            {
                new Log()
                {
                    Name = "teste",
                    Description = "falha crítica",
                    Environment = "prod",
                    Type = "LoginError",
                    UserId = 1
                },
                new Log()
                {
                    Name = "teste",
                    Description = "falha crítica",
                    Environment = "prod",
                    Type = "LoginError",
                    UserId = 1
                },
            };

            repositoryMock.Setup(x => x.GetAll()).Returns(logList);

            var logService = new LogService(repositoryMock.Object);

            var expectedLogList = logService.GetAll();

            Assert.Equal(expectedLogList, logList);
        }

        [Fact]
        public void shoud_get_log_by_id()
        {
            var repositoryMock = new Mock<ILogRepository>();

            var log = new Log()
            {
                Name = "teste",
                Description = "falha crítica",
                Environment = "prod",
                Type = "LoginError",
                UserId = 1
            };

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(log);

            var logService = new LogService(repositoryMock.Object);

            var expectedLog = logService.GetById(1);

            Assert.Equal(expectedLog, log);
        }

        [Fact]
        public void shoud_fail_when_log_doesnt_exists_get_user_by_id()
        {
            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((Log)null);

            var logService = new LogService(repositoryMock.Object);

            Assert.Throws<NotFoundException>(() => logService.GetById(1));
        }
    }
}
