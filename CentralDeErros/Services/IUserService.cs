using CentralDeErros.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public interface IUserService
    {

        public IList<User> GetAll();

        public User GetById(int id);

        public int Save(User user);

        public void Update(User user);

    }
}
