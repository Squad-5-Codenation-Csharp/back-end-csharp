using CentralDeErros.Api.Models;
using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Repository;
using CentralDeErros.Repositories;
using CentralDeErros.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CentralDeErrosTests.Business.Services
{
    public class UserServiceTest
    {

        [Fact]
        public void shoud_get_all_users()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var authrepositoryMock = new Mock<IAuthService>();

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

            repositoryMock.Setup(x => x.GetAll()).Returns(userList);

            var userService = new UserService(repositoryMock.Object, authrepositoryMock.Object);

            var expectedUserList = userService.GetAll();

            Assert.Equal(expectedUserList, userList);
        }

        [Fact]
        public void shoud_get_user_by_id()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var authrepositoryMock = new Mock<IAuthService>();

            var user = new User()
            {
                Name = "teste",
                Email = "teste4@teste.com",
                Password = "0197ebmm",
            };

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            var userService = new UserService(repositoryMock.Object, authrepositoryMock.Object);

            var expectedUser = userService.GetById(1);

            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public void shoud_fail_when_user_doesnt_exists_get_user_by_id()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var authrepositoryMock = new Mock<IAuthService>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((User)null) ;

            var userService = new UserService(repositoryMock.Object, authrepositoryMock.Object);

            Assert.Throws<NotFoundException>(() => userService.GetById(1));
        }

        [Fact]
        public void shoud_correctly_create_user()
        {
            var user = new User()
            {
                Name = "teste",
                Email = "teste4@teste.com",
                Password = "0197ebmm",
            };

            var userId = 1;

            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock.Setup(x => x.Save(It.IsAny<User>())).Callback(() =>
            {
                user.Id = userId;
            }).Returns(user);


            var userService = new UserService(repositoryMock.Object, new AuthService());
            var createdUserId = userService.Save(user);

            Assert.Equal(createdUserId, userId);
        }

        [Fact]
        public void shoud_fail_when_create_user()
        {
            var user = new User()
            {
                Name = "teste",
                Email = "teste4@teste.com",
                Password = "0197ebmm",
            };

            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock.Setup(x => x.Save(It.IsAny<User>())).Throws(new DuplicatedEntity("Falha ao criar usuário"));

            var userService = new UserService(repositoryMock.Object, new AuthService());

            Assert.Throws<DuplicatedEntity>(() => userService.Save(user));
        }

        [Fact]
        public void shoud_correctly_update_user()
        {
            var user = new User()
            {
                Id = 1,
                Name = "teste",
                Active = false
            };

            var UserToUpdate = new User()
            {
                Id = 1,
                Name = "nome antigo",
                Email = "teste4@teste.com",
                Active = true,
                Password = "hashed.password.key"
            };

            var userId = 1;

            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(UserToUpdate);

            repositoryMock.Setup(x => x.Update(It.IsAny<User>())).Callback(() =>
            {
                UserToUpdate.Name = user.Name;
                UserToUpdate.Active = user.Active;
            }).Returns(UserToUpdate);


            var userService = new UserService(repositoryMock.Object, new AuthService());
            
            userService.Update(user);
        }

        [Fact]
        public void shoud_fail_when_user_doesnt_exists_update_user()
        {
            var user = new User()
            {
                Id = 1,
                Name = "teste",
                Active = false
            };

            User UserToUpdate = null;

            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(UserToUpdate);

            var userService = new UserService(repositoryMock.Object, new AuthService());

            Assert.Throws<NotFoundException>(() => userService.Update(user));
        }

        [Fact]
        public void shoud_fail_because_cannot_get_user_by_email_login_user()
        {
            var userEmail = "teste4@teste.com";
            var userPassword = "password";

            User user = null;

            var repositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            repositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);

            var userService = new UserService(repositoryMock.Object, new AuthService());

            Assert.Throws<NotFoundException>(() => userService.Login(userEmail, userPassword));
        }


        [Fact]
        public void shoud_fail_because_password_upgrade_login_user()
        {
            var userEmail = "teste4@teste.com";
            var userPassword = "password";

            var user = new User()
            {
                Id = 1,
                Name = "nome antigo",
                Email = "teste4@teste.com",
                Active = true,
                Password = "hashed.password.key"
            };

            var repositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            repositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);
            authServiceMock.Setup(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>())).Returns((true, true));

            var userService = new UserService(repositoryMock.Object, authServiceMock.Object);

            Assert.Throws<InvalidOperationException>(() => userService.Login(userEmail, userPassword));
        }

        [Fact]
        public void shoud_fail_because_password_error_login_user()
        {
            var userEmail = "teste4@teste.com";
            var userPassword = "password";

            var user = new User()
            {
                Id = 1,
                Name = "nome antigo",
                Email = "teste4@teste.com",
                Active = true,
                Password = "hashed.password.key"
            };

            var repositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            repositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);
            authServiceMock.Setup(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>())).Returns((false, false));

            var userService = new UserService(repositoryMock.Object, authServiceMock.Object);

            Assert.Throws<ArgumentException>(() => userService.Login(userEmail, userPassword));
        }

        [Fact]
        public void shoud_login_user()
        {
            var userEmail = "teste4@teste.com";
            var userPassword = "password";

            var user = new User()
            {
                Id = 1,
                Name = "nome antigo",
                Email = "teste4@teste.com",
                Active = true,
                Password = "hashed.password.key"
            };

            var repositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            repositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(user);
            authServiceMock.Setup(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>())).Returns((true, false));

            var userService = new UserService(repositoryMock.Object, authServiceMock.Object);

            var token = userService.Login(userEmail, userPassword);

            Assert.NotNull(token);
        }
    }
}
