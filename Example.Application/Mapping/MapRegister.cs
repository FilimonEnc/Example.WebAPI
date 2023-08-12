using Mapster;

using Example.Application.Mapping.Configuration;

namespace Example.Application.Mapping
{
    public class MapRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            ExampleMappingConfiguration.Configure(config);
        }
    }
}