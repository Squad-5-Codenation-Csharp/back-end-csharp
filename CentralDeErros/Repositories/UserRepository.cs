using CentralDeErros.Api.Data;
using CentralDeErros.Api.Models;
using CentralDeErros.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CentralDeErrosApiContext context) : base(context)
        {

        }
    }
}
