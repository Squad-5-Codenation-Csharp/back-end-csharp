using CentralDeErros.Api.Data;
using CentralDeErros.Api.Models;
using CentralDeErros.Data.Repository;
using CentralDeErros.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.Business.Repositories
{
    public class UserRepositoryTest
    {
        [Fact]
        public void Must_get_the_user_by_id()
        {
            var createdUser = new User()
            {
                Name = "teste",
                Email = "teste4@teste.com",
                Password = "0197ebmm",
            };

            var baseRepository = new Mock<IBaseRepository<User>>();

            var contextOptions = new DbContextOptions<CentralDeErrosApiContext>();

            baseRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(createdUser);

            var mockedContext = new Mock<CentralDeErrosApiContext>(contextOptions);
            var mockedUserDb = new Mock<DbSet<User>>();

            mockedContext.Setup(x => x.Set<User>()).Returns(mockedUserDb.Object);

            IUserRepository userRepository = new UserRepository(mockedContext.Object);

            var expectedUser = userRepository.GetById(1);

            Assert.NotNull(expectedUser);
        }
    }
}
