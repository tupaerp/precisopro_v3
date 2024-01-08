using PrecisoPRO.Models;

namespace PrecisoPRO.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAll();

        Task<Cliente> GetByIdAsync(int id);

        Task<Cliente> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Cliente>> GetEmpresaByCity(string cidade);

        Task<IEnumerable<Cliente>> GetAllAsyncNoTracking();

        bool Adicionar(Cliente cliente);


        bool Update(Cliente cliente);

        bool Delete(Cliente cliente);
        bool Save();
    }
}
