using CentralDeErros.Api.Models;
using CentralDeErros.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class LogService
    {
        private readonly ILogRepository repository;

        public LogService(ILogRepository repository)
        {
            this.repository = repository;
        }

        public IList<Log> GetAll()
        {
            var LogList = repository.GetAll();

            return LogList;
        }

        public Log GetById(int id)
        {
            var log = repository.GetById(id);

            return log;
        }

        public int Save(Log log)
        {
            var createdLog = repository.Save(log);
            return createdLog.Id;
        }

        public void Update(Log log)
        {
            repository.Update(log);
        }
    }
}
