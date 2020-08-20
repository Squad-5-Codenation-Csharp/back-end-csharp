using CentralDeErros.RequestValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace CentralDeErrosTests.API.RequestValidations
{
    public class CreateUserRequestValidationTest : RequestValidationsBaseSetup
    {
        [Fact]
        void shoud_validate_correct_input_with_valid_data()
        {
            var validCreateUserRequestValidation = new CreateUserRequestValidation()
            {
                Name = "Exemplo",
                Email = "email@email.com",
                Password = "senha!123"
            };

            var result = ValidateModel(validCreateUserRequestValidation);

            Assert.True(result.Count == 0);
        }

        [Fact]
        void shoud_validate_no_name_data()
        {
            var validCreateUserRequestValidation = new CreateUserRequestValidation()
            {
                Email = "email@email.com",
                Password = "senha!123"
            };

            var result = ValidateModel(validCreateUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("É necessário informar o nome do usuário"));
        }

        [Fact]
        void shoud_validate_no_email_data()
        {
            var validCreateUserRequestValidation = new CreateUserRequestValidation()
            {
                Name = "Nome",
                Password = "senha!123"
            };

            var result = ValidateModel(validCreateUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("É necessário informar o email do usuário"));
        }

        [Fact]
        void shoud_validate_invalid_email_data()
        {
            var validCreateUserRequestValidation = new CreateUserRequestValidation()
            {
                Name = "Nome",
                Email= "email invalido",
                Password = "senha!123"
            };

            var result = ValidateModel(validCreateUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Email inválido"));
        }

        [Fact]
        void shoud_validate_no_password_data()
        {
            var validCreateUserRequestValidation = new CreateUserRequestValidation()
            {
                Name = "Nome",
                Email = "email@email.com",
            };

            var result = ValidateModel(validCreateUserRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Password") && v.ErrorMessage.Contains("É necessário informar a senha do usuário"));
        }
    }
}
