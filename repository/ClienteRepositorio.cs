using APIClientes.Data;
using APIClientes.Model;
using APIClientes.Model.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClientes.repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ClienteRepositorio(ApplicationDbContext db, IMapper mapper)
        {

            _db = db;
            _mapper = mapper;
        }

        async Task<ClienteDto> IClienteRepositorio.CreateUpdate(ClienteDto clienteDto)
        {
            Cliente cliente = _mapper.Map<ClienteDto, Cliente>(clienteDto);
            if (cliente.Id > 0)
            {
                _db.Clientes.Update(cliente);
            } else
            {
                await _db.Clientes.AddAsync(cliente);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Cliente, ClienteDto>(cliente);
        }

        async Task<bool> IClienteRepositorio.DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = await _db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return false;
                }

                _db.Clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        async Task<List<ClienteDto>> IClienteRepositorio.GetClientes()
        {
            List<Cliente> lista = await _db.Clientes.ToListAsync();

            return _mapper.Map<List<ClienteDto>>(lista);
        }

        async Task<ClienteDto> IClienteRepositorio.GetClientesById(int id)
        {
            Cliente cliente = await _db.Clientes.FindAsync(id);

            return _mapper.Map<ClienteDto>(cliente);
        }
    }
}
