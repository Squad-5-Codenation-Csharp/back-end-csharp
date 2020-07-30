using CentralDeErros.Api.Models;
using CentralDeErros.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
