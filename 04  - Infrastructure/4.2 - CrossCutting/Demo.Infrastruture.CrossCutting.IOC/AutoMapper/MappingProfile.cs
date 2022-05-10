using AutoMapper;
using Demo.Application.Demo.DTO.DTO;
using Demo.Application.Demo.DTO.Request;
using Demo.Domain.Entities;

namespace Demo.Infrastruture.CrossCutting.IOC.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            
            CreateMap<CreateUserRequest, UserDTO>().ReverseMap();
            CreateMap<UpdateUserRequest, UserDTO>().ReverseMap();
        }
    }
}
