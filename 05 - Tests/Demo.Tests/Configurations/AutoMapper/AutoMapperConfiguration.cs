using AutoMapper;
using Demo.Application.Demo.DTO.DTO;
using Demo.Application.Demo.DTO.Request;
using Demo.Infrastruture.CrossCutting.IOC.AutoMapper;

namespace Demo.Tests.Configurations.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());

                cfg.CreateMap<CreateUserRequest, UserDTO>()
                    .ReverseMap();

                cfg.CreateMap<UpdateUserRequest, UserDTO>()
                    .ReverseMap();
            });

            return autoMapperConfig.CreateMapper();
        }
    }
}
