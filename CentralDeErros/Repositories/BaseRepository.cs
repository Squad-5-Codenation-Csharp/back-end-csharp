using CentralDeErros.Api.Data;
using CentralDeErros.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralDeErros.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly CentralDeErrosApiContext context;

        public BaseRepository(CentralDeErrosApiContext context)
        {
            this.context = context;
        }

        List<T> IBaseRepository<T>.GetAll()
        {
            return context.Set<T>().ToList();
        }

        T IBaseRepository<T>.GetById(int id)
        {
            return context.Set<T>().FirstOrDefault(item => item.Id == id);
        }
    }
}
