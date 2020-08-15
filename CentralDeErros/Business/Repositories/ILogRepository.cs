using CentralDeErros.Api.Models;
using CentralDeErros.Business.Models;
using CentralDeErros.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Data.Interfaces
{
    public interface ILogRepository : IBaseRepository<Log>
    {
        public IList<LogDistribuition> GetLogDistribuition(string? env);

        public List<Log> GetAll(string? env, string? type);
    }

}
