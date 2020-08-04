using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.RequestValidations;
using CentralDeErros.ResponseModel;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogService service;
        private IMapper mapper;

        public LogController(ILogService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<LogResponseModel>> GetAll()
        {
            var logList = service.GetAll();

            return Ok(mapper.Map<IList<LogResponseModel>>(logList));
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] CreateLogRequestValidation log)
        {
            var UserId = service.Save(mapper.Map<Log>(log));

            return Ok(UserId);
        }
    }
}
