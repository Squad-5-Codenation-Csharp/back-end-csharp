using CentralDeErros.Api.Models;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository): base(repository)
        {
            this.repository = repository;
        }
    }
}
