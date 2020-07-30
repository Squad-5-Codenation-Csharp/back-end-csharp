using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralDeErros.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErros.Api.Data
{
    public class CentralDeErrosApiContext : DbContext
    {
        public CentralDeErrosApiContext (DbContextOptions<CentralDeErrosApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}
