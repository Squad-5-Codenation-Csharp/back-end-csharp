using CentralDeErros.RequestValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.API.RequestValidations
{
    public class CreateLogRequestValidationTest: RequestValidationsBaseSetup
    {
        [Fact]
        void shoud_validate_correct_input_with_valid_data()
        {
            var validCreatelogRequestValidation = new CreateLogRequestValidation()
            {
                Name = "Exemplo",
                Description = "Descrição",
                Environment = "prod",
                Type = "LoginError",
                UserId = 1
            };

            var result = ValidateModel(validCreatelogRequestValidation);

            Assert.True(result.Count == 0);
        }

        [Fact]
        void shoud_validate_no_name_data()
        {
            var validCreatelogRequestValidation = new CreateLogRequestValidation()
            { 
                Description = "Descrição",
                Environment = "prod",
                Type = "LoginError",
                UserId = 1
            };

            var result = ValidateModel(validCreatelogRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("É necessário informar o nome do log"));
        }

        [Fact]
        void shoud_validate_no_description_data()
        {
            var validCreatelogRequestValidation = new CreateLogRequestValidation()
            {
                Name = "Teste",
                Environment = "prod",
                Type = "LoginError",
                UserId = 1
            };

            var result = ValidateModel(validCreatelogRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Description") && v.ErrorMessage.Contains("É necessário informar a descrição do log"));
        }

        [Fact]
        void shoud_validate_no_env_data()
        {
            var validCreatelogRequestValidation = new CreateLogRequestValidation()
            {
                Name = "Teste",
                Description = "Descrição",
                Type = "LoginError",
                UserId = 1
            };

            var result = ValidateModel(validCreatelogRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Environment") && v.ErrorMessage.Contains("É necessário informar o ambiente do log"));
        }

        [Fact]
        void shoud_validate_no_type_data()
        {
            var validCreatelogRequestValidation = new CreateLogRequestValidation()
            {
                Name = "Teste",
                Description = "Descrição",
                Environment = "prod",
                UserId = 1
            };

            var result = ValidateModel(validCreatelogRequestValidation);

            Assert.Contains(result, v => v.MemberNames.Contains("Type") && v.ErrorMessage.Contains("É necessário informar o tipo do log"));
        }
    }
}