using CentralDeErros.Api.Data;
using CentralDeErros.Api.Models;
using CentralDeErros.Business.Models;
using CentralDeErros.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CentralDeErros.Data.Repository
{
    public class LogRepository : BaseRepository<Log> , ILogRepository
    {
        public LogRepository(CentralDeErrosApiContext context) : base(context)
        {

        }

        public IList<LogDistribuition> GetLogDistribuition(string? env)
        {
            return context.Log
                .Where(x => env != null? x.Environment == env : true)
                .GroupBy(x => x.Type)
                .Select(group => new  LogDistribuition
                {
                    Env = group.Key,
                    Count = group.Count()
                })
                .ToArray();
        }

        public List<Log> GetAll(string env, string type, int? userId)
        {
            return context.Log
                .Where(x => 
                    (userId != null? x.UserId == userId : true) &&
                    (env != null ? x.Environment == env : true) && 
                    (type != null ? x.Type == type : true) &&
                    x.Active == true)
                .ToList();
        }
    }
}
