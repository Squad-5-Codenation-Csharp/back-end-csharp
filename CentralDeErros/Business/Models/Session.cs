using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.Models
{
    public class Session
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public User User { get; set; }
    }
}
