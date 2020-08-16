using CentralDeErros.Api.Models;
using CentralDeErros.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.Business.Services
{
    public class AuthServiceTest
    {
        [Fact]
        public void should_generate_random_hash_based_on_password()
        {
            var password = "senha!1234";

            var authService = new AuthService();

            var hashedPassword = authService.Hash(password);

            var structure = hashedPassword.Split(".", 3);

            Assert.NotNull(hashedPassword);
            Assert.NotEqual(password, hashedPassword);
            Assert.Equal(3, structure.Length);
        }

        [Fact]
        public void should_compare_password_with_hash()
        {
            var password = "senha!1234";
            var user = new User()
            {
                Name = "Teste",
                Email = "teste@example.com",
                Password = "10000.dUrL+De7gIVM2xgaCW1KWQ==.ElApWkRrGxX4ObXqGuwP9DbxmdkfsrQKncBuTAUBiKI="
            };

            var authService = new AuthService();

            (var validPassword, var needUpgrade) = authService.ComparePassword(user.Password, password);

            Assert.True(validPassword);
            Assert.False(needUpgrade);
        }

        [Fact]
        public void should_fail_because_invalid_hash()
        {
            var password = "senha!1234";
            var user = new User()
            {
                Name = "Teste",
                Email = "teste@example.com",
                Password = "dUrL+De7gIVM2xgaCW1KWQ==.ElApWkRrGxX4ObXqGuwP9DbxmdkfsrQKncBuTAUBiKI="
            };

            var authService = new AuthService();
            Assert.Throws<FormatException>(() => authService.ComparePassword(user.Password, password));
        }

        [Fact]
        public void should_create_user_token()
        {
            var user = new User()
            {
                Name = "Teste",
                Email = "teste@example.com",
            };

            var authService = new AuthService();

            var userToken = authService.Authenticate(user);

            Assert.NotNull(userToken);
        }
    }
}
