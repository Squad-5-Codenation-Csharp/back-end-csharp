using System;
using Xunit;
using CentralDeErros;
using CentralDeErros.RequestValidations;
using System.ComponentModel.DataAnnotations;

namespace CentralDeErrosTests.RequestValidations
{
    public class LoginUserRequestValidationTest
    {
        [Fact]
        public void Should_correct_validate_login_request_values()
        {
            var loginRequestValidation = new LoginUserRequestValidation
            {
                Email = "teste@teste.com",
                Password = "senha123"
            };

            Assert.NotNull(loginRequestValidation);
        }
    }
}
