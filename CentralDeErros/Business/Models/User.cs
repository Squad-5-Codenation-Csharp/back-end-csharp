using CentralDeErros.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password{ get; set; }

        public bool Active { get; set; } = true;

        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
