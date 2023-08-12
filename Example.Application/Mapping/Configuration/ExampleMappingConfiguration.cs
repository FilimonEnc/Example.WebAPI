using Example.Application.Models.Examples;
using Example.Core.Entities;

using Mapster;

namespace Example.Application.Mapping.Configuration
{
    public static class ExampleMappingConfiguration
    {
        public static void Configure(TypeAdapterConfig config)
        {
            config.NewConfig<ExampleEntity, ExampleModel>()
                .Map(dest => dest.Example, s => s.ExampleName != null);

        }


    }
}
