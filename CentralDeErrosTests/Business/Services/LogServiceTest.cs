using CentralDeErros.Api.Models;
using CentralDeErros.Business.Models;
using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.Business.Services
{
    public class LogServiceTest
    {
        [Fact]
        public void should_get_all_logs()
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
        public void should_get_all_logs_with_env()
        {
            var repositoryMock = new Mock<ILogRepository>();

            var env = "dev";

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
                    Environment = "dev",
                    Type = "LoginError",
                    UserId = 1
                },
            };

            repositoryMock.Setup(x => x.GetAll(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>())).Returns(logList.Where(x => x.Environment == "dev").ToList());

            var logService = new LogService(repositoryMock.Object);

            var expectedLogList = logService.GetAll(env, null, null);

            Assert.Equal(1, expectedLogList.Count);
        }

        [Fact]
        public void should_get_all_logs_with_type()
        {
            var repositoryMock = new Mock<ILogRepository>();

            var type = "LoginError";

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
                    Environment = "dev",
                    Type = "AuthError",
                    UserId = 1
                },
            };

            repositoryMock.Setup(x => x.GetAll(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>())).Returns(logList.Where(x => x.Type == "LoginError").ToList());

            var logService = new LogService(repositoryMock.Object);

            var expectedLogList = logService.GetAll(null, type, null);

            Assert.Equal(1, expectedLogList.Count);
        }

        [Fact]
        public void should_get_log_by_id()
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
        public void should_fail_when_log_doesnt_exists_get_user_by_id()
        {
            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((Log)null);

            var logService = new LogService(repositoryMock.Object);

            Assert.Throws<NotFoundException>(() => logService.GetById(1));
        }

        [Fact]
        public void should_correctly_create_log()
        {
            var log = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var logId = 1;

            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.Save(It.IsAny<Log>())).Callback(() =>
            {
                log.Id = logId;
            }).Returns(log);


            var logService = new LogService(repositoryMock.Object);
            var createdLogId = logService.Save(log);

            Assert.Equal(createdLogId, logId);
        }

        [Fact]
        public void should_fail_when_create_log()
        {
            var log = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.Save(It.IsAny<Log>())).Throws(new DuplicatedEntity("Erro ao inserir novo Log: já existe uma entrada com um ou mais atributos informados"));

            var logService = new LogService(repositoryMock.Object);

            Assert.Throws<DuplicatedEntity>(() => logService.Save(log));
        }

        [Fact]
        public void should_correctly_update_log()
        {
            var log = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var logUpdated = new Log()
            {
                Id = 1,
                Name = "Log_Name_Update",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.Update(It.IsAny<Log>())).Callback(() =>
            {
                log.Name = "Log_Name_Update";
                log.Description = "Log_Description";

            }).Returns(log);

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(log);

            var logService = new LogService(repositoryMock.Object);

            logService.Update(log);

            Assert.Equal("Log_Name_Update", logUpdated.Name);
        }

        [Fact]
        public void should_fail_when_log_doesnt_exists_update_log()
        {
            var log = new Log()
            {
                Id = 1,
                Name = "Log_Name",
                Description = "Log_Description",
                Environment = "dev",
                Type = "Error",
                UserId = 1
            };

            Log logNull = null;

            var repositoryMock = new Mock<ILogRepository>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(logNull);

            var logService = new LogService(repositoryMock.Object);

            Assert.Throws<NotFoundException>(() => logService.Update(log));
        }

        [Fact]
        public void should_get_log_statistics()
        {
            var repositoryMock = new Mock<ILogRepository>();

            var logDistribuition = new List<LogDistribuition>(){
                new LogDistribuition()
                {
                    Env = "Dev",
                    Count = 5
                },
                new LogDistribuition()
                {
                    Env = "Prod",
                    Count = 6
                }
            };

            repositoryMock.Setup(x => x.GetLogDistribuition(It.IsAny<string>())).Returns(logDistribuition);

            var logService = new LogService(repositoryMock.Object);

            var expectedLogList = logService.GetLogsDistribuition(It.IsAny<string>());

            Assert.Equal(logDistribuition, expectedLogList);
        }
    }
}
