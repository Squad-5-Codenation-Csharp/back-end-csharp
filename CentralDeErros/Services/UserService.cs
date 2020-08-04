using CentralDeErros.Api.Models;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IList<User> GetAll()
        {
            var UserList = repository.GetAll();

            return UserList;
        }

        public User GetById(int id)
        {
            var user = repository.GetById(id);

            return user;
        }

        public int Save(User user)
        {
            var createdUser = repository.Save(user);
            return createdUser.Id;
        }

        public void Update(User user)
        {
            repository.Update(user);
        }

    }
}
