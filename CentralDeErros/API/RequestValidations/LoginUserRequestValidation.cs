using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.RequestValidations
{
    public class LoginUserRequestValidation
    {
        [Required(ErrorMessage = "É necessário informar o email do usuário")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar a senha do usuário")]
        public string Password { get; set; }
    }
}
