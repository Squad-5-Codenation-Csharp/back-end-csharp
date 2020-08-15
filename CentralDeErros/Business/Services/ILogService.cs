using CentralDeErros.Api.Models;
using CentralDeErros.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Services
{
    public interface ILogService
    {
        public IList<Log> GetAll(string? env, string? type, int? userId);

        public Log GetById(int id);

        public int Save(Log log);

        public void Update(Log log);

        public IList<LogDistribuition> GetLogsDistribuition(string? env);
    }
}
