using AutoMapper;

namespace Demo.Infrastruture.CrossCutting.IOC.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IConfigurationProvider Configure()
        {
            return new MapperConfiguration(ConfigAction);
        }

        public static Action<IMapperConfigurationExpression> ConfigAction = cfg => { };
    }
}
