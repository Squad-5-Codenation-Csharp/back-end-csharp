﻿using System.Collections.Generic;
using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.RequestValidations;
using CentralDeErros.ResponseModel;
using CentralDeErros.Services;
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
        public ActionResult<IList<LogResponseModel>> GetAll([FromQuery] string? env, [FromQuery] string? type)
        {
            var logList = service.GetAll(env, type);

            return Ok(mapper.Map<IList<LogResponseModel>>(logList));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<LogResponseModel> GetById(int id)
        {
            var log = service.GetById(id);

            return Ok(mapper.Map<LogResponseModel>(log));
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] CreateLogRequestValidation log)
        {
            var UserId = service.Save(mapper.Map<Log>(log));

            return Ok(UserId);
        }

        [HttpGet]
        [Route("distribuition")]
        public ActionResult<IList<dynamic>> LogDistribuition([FromQuery] string? env)
        {
            var logDistribuition = service.GetLogsDistribuition(env);

            return Ok(logDistribuition);
        }
    }
}
