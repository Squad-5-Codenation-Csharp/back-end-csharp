using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.ResponseModel
{
    public class LogResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }

        public string Type { get; set; }

        public bool Archieved { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
    }
}
