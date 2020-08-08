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

        public UserService(IUserRepository repository): base(repository)
        {
        }


        public new void Update(User user)
        {
            var getUser = GetById(user.Id);

            if (user.Active != getUser.Active)
                getUser.Active = user.Active;
            if (user.Name != null &&  user.Name != getUser.Name)
                getUser.Name = user.Name;
            if (user.Email != null &&  user.Email != getUser.Email)
                getUser.Email = user.Email;

            _repository.Update(getUser);
        }
    }
}
