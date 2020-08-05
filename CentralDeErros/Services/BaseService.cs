using CentralDeErros.Data.Interfaces;
using CentralDeErros.Data.Repository;
using System;
using System.Collections.Generic;
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

            return itemList;
        }

        public T GetById(int id)
        {
            var item = _repository.GetById(id);

            return item;
        }

        public int Save(T item)
        {
            var createdItem = _repository.Save(item);
            return createdItem.Id;
        }

        public void Update(T item)
        {
            _repository.Update(item);
        }
    }
}
