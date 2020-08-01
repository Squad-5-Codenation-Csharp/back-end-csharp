using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.RequestValidations
{
    public class CreateUserRequestValidation
    {
        [Required(ErrorMessage = "É necessário informar o nome do usuário")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário informar o email do usuário")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar a senha do usuário")]
        public string Password { get; set; }
    }
}
