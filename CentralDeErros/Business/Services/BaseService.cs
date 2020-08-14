using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Interfaces;
using CentralDeErros.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class BaseService<T> where T : class, IEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this._repository = repository;
        }

        public IList<T> GetAll()
        {
            var itemList = _repository.GetAll();

            if (itemList == null)
                throw new NotFoundException($"Nenhum item do tipo {typeof(T).Name} encontrado");

            return itemList;
        }

        public T GetById(int id)
        {
            var item = _repository.GetById(id);

            if (item == null)
                throw new NotFoundException($"Nenhum {typeof(T).Name} encontrado para o id informado");

            return item;
        }

        public int Save(T item)
        {
            try
            {
                var createdItem = _repository.Save(item);
                return createdItem.Id;
            } catch (DbUpdateException)
            {
                throw new DuplicatedEntity($"Erro ao inserir novo {typeof(T).Name}: já existe uma entrada com um ou mais atributos informados");
            }
        }

        public void Update(T item)
        {
            _repository.Update(item);
        }
    }
}
