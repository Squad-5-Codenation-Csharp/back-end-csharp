﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password{ get; set; }

        public bool Status{ get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public Session Session { get; set; }
    }
}
