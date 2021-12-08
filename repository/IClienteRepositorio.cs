using APIClientes.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClientes.repository
{
    public interface IClienteRepositorio
    {

        Task<List<ClienteDto>> GetClientes();

        Task<ClienteDto> GetClientesById(int id);

        Task<ClienteDto> CreateUpdate(ClienteDto clienteDto);

        Task<bool> DeleteCliente(int id);

    }
}
