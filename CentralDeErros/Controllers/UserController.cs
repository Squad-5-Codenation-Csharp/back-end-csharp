using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.ResponseModel;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService service;
        private IMapper mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this.service = service;
        }


        [HttpGet]
        public ActionResult<IList<UserResponseModel>> GetAll()
        {
            var UserList = service.GetAll();

            return Ok(mapper.Map<IList<UserResponseModel>>(UserList));
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] User user)
        {
            var UserId = service.Save(user);

            return Ok(UserId);
        }
    }
}
