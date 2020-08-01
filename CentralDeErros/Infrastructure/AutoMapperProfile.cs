using AutoMapper;
using CentralDeErros.Api.Models;
using CentralDeErros.ResponseModel;

namespace CentralDeErros.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponseModel>().ReverseMap();
        }
    }
}
