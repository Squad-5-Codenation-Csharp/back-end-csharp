using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.RequestValidations
{
    public class UpdateUserRequestValidation
    {
        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Boolean Status { get; set; }
    }
}
