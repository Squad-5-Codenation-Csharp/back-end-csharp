using CentralDeErros.Api.Models;
using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IAuthService authService;
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository, IAuthService authService): base(repository)
        {
            this.authService = authService;
            this.repository = repository;
        }

        public new int Save(User user)
        {
            user.Password = authService.Hash(user.Password);
            
            try
            {
                var createdUser= _repository.Save(user);
                return createdUser.Id;

            } catch (DbUpdateException)
            {
                throw new DuplicatedEntity($"Erro ao inserir novo usuário: já existe uma entrada com um ou mais atributos informados");
            }
            
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

        public object Login(string email, string password)
        {
            var user = GetUserByEmail(email);

            (bool ValidPassword, bool NeedUpgrade) = authService.ComparePassword(user.Password, password);

            if (!ValidPassword)
                throw new ArgumentException("Senha inválida para o usuário informado");
            if (ValidPassword && NeedUpgrade)
                throw new InvalidOperationException("A senha precisa ser atualizada");

            var token = authService.Authenticate(user);

            user.Password = "";

            return new
            {
                user,
                token
            };
        }

        private User GetUserByEmail(string email)
        {
            var user = repository.GetUserByEmail(email);

            if (user == null)
                throw new NotFoundException("Nenhum usuário encontrado para o email informado");

            return user;
        }
    }
}
