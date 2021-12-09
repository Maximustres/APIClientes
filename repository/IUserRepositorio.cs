using APIClientes.Model;
using System.Threading.Tasks;

namespace APIClientes.repository
{
    public interface IUserRepositorio
    {
        Task<int> Register(User user, string password);

        Task<string> Login(string userName, string password);

        Task<bool> UserExist(string userName);
    }
}
