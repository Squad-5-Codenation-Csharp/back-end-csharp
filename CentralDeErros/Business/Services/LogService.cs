using CentralDeErros.Api.Models;
using CentralDeErros.Business.Models;
using CentralDeErros.Business.Utils;
using CentralDeErros.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public class LogService : BaseService<Log>, ILogService
    {
        private readonly ILogRepository repository;
        
        public LogService(ILogRepository repository): base(repository)
        {
            this.repository = repository;
        }

        public IList<Log> GetAll(string? env, string? type, int? userId)
        {
            var logList = repository.GetAll(env, type, userId);

            if (logList.Count == 0)
                throw new NotFoundException("Nenhum log encontrado");

            return logList;
        }

        public new void Update(Log log)
        {
            var getLog = GetById(log.Id);

            if (log.Name != getLog.Name)
                getLog.Name = log.Name;

            if (log.Description != getLog.Description)
                getLog.Description = log.Description;

            if (log.Environment != getLog.Environment)
                getLog.Environment = log.Environment;

            if (log.Type != getLog.Type)
                getLog.Type = log.Type;

            if (log.Archieved != getLog.Archieved)
                getLog.Archieved = log.Archieved;

            if (log.Active != getLog.Active)
                getLog.Active = log.Active;


            _repository.Update(getLog);
        }

        public IList<LogDistribuition> GetLogsDistribuition(string? env)
        {
            var logDistribuition = repository.GetLogDistribuition(env);

            return logDistribuition;
        }
    }
}
