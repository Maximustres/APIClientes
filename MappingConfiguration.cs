using APIClientes.Model;
using APIClientes.Model.Dto;
using AutoMapper;

namespace APIClientes
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteDto, Cliente>();
                config.CreateMap<Cliente, ClienteDto>();
            });

            return mappingConfig;
        }
    }
}
