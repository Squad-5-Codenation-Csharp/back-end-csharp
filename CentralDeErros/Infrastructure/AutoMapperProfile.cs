using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.RequestValidations;
using CentralDeErros.ResponseModel;

namespace CentralDeErros.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<CreateUserRequestValidation, User>().ReverseMap();
            CreateMap<UpdateUserRequestValidation, User>().ReverseMap();
            CreateMap<Log, LogResponseModel>().ReverseMap();
            CreateMap<CreateLogRequestValidation, Log>().ReverseMap();
        }
    }
}
