using CentralDeErros.Api.Models;
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
    }
}
