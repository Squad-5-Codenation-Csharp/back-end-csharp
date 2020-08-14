using CentralDeErros.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Data.Repository
{
    public interface IBaseRepository<T> where T : class , IEntity
    {
        T GetById(int id);

        List<T> GetAll();

        T Save(T entity);

        T Update(T entity);
    }
}
