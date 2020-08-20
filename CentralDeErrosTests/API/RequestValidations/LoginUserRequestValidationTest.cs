using CentralDeErros.RequestValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.API.RequestValidations
{
    public class LoginUserRequestValidationTest : RequestValidationsBaseSetup
    {
        [Fact]
        void shoud_validate_correct_input_with_valid_data()
        {
            var validLoginUserRequestValidation = new LoginUserRequestValidation()
            {
                Email = "email@email.com",
                Password = "senha!123"
            };

            var result = ValidateModel(validLoginUserRequestValidation);

            Assert.True(result.Count == 0);
        }

        [Fact]
        void shoud_validate_no_email_data()
        {
            var validLoginUserRequestValidation = new LoginUserRequestValidation()
            {
                Password = "senha!123"
            };

            var result = ValidateModel(validLoginUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("É necessário informar o email do usuário"));
        }

        [Fact]
        void shoud_validate_invalid_email_data()
        {
            var validLoginUserRequestValidation = new LoginUserRequestValidation()
            {
                Email = "invalid email",
                Password = "senha!123"
            };

            var result = ValidateModel(validLoginUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Email inválido"));
        }

        [Fact]
        void shoud_validate_no_password_data()
        {
            var validLoginUserRequestValidation = new LoginUserRequestValidation()
            {
                Email = "teste@teste.com"
            };

            var result = ValidateModel(validLoginUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Password") && v.ErrorMessage.Contains("É necessário informar a senha do usuário"));
        }
    }
}
