using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.RequestValidations;
using CentralDeErros.ResponseModel;
using CentralDeErros.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService service;
        private IMapper mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IList<UserResponseModel>> GetAll()
        {
            var UserList = service.GetAll();

            return Ok(mapper.Map<IList<UserResponseModel>>(UserList));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserResponseModel> GetById(int id)
        {
            var user = service.GetById(id);

            return Ok(mapper.Map<UserResponseModel>(user));
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult<int> Post([FromBody] CreateUserRequestValidation user)
        {
            var UserId = service.Save(mapper.Map<User>(user));

            return Ok(UserId);
        }

        [HttpPut]
        public ActionResult Update([FromBody] UpdateUserRequestValidation user)
        {
            service.Update(mapper.Map<User>(user));

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<bool> Login([FromBody] LoginUserRequestValidation userParams)
        {
            var result = service.Login(userParams.Email, userParams.Password);

            return Ok(result);
        }
    }
}
