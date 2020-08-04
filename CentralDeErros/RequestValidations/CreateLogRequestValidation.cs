using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.RequestValidations
{
    public class CreateLogRequestValidation
    {
        [Required(ErrorMessage = "É necessário informar o nome do log")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário informar a descrição do log")]
        public string Description { get; set; }

        [Required(ErrorMessage = "É necessário informar o ambiente do log")]
        public string Environment { get; set; }

        [Required(ErrorMessage = "É necessário informar o tipo do log")]
        public string Type { get; set; }

        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public int UserId { get; set; }
    }
}
