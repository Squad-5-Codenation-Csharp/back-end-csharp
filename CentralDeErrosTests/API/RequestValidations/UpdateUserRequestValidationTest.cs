using CentralDeErros.RequestValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CentralDeErrosTests.API.RequestValidations
{
    public class UpdateUserRequestValidationTest : RequestValidationsBaseSetup
    {
        [Fact]
        void shoud_validate_correct_input_with_valid_data()
        {
            var validUpdateUserRequestValidation = new UpdateUserRequestValidation()
            {
                Id = 1,
                Name = "Exemplo",
                Email = "email@email.com",
                Active = true
            };

            var result = ValidateModel(validUpdateUserRequestValidation);

            Assert.True(result.Count == 0);
        }
    }
}
