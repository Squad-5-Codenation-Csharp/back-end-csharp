using CentralDeErros.Api.Data;
using CentralDeErros.Api.Models;
using CentralDeErros.Data.Interfaces;

namespace CentralDeErros.Data.Repository
{
    public class LogRepository : BaseRepository<Log> , ILogRepository
    {
        public LogRepository(CentralDeErrosApiContext context) : base(context)
        {

        }
    }
}
