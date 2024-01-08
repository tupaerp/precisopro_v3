using PrecisoPRO.Models;

namespace PrecisoPRO.Interfaces
{
    public interface ICndClienteEstadual
    {
        Task<IEnumerable<CndClienteEstadual>> GetAll();

        Task<CndClienteEstadual> GetByIdAsync(int id);

        Task<CndClienteEstadual> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<CndClienteEstadual>> GetClienteByCity(string uf);

        Task<IEnumerable<CndClienteEstadual>> GetAllAsyncNoTracking();

        bool Adicionar(CndClienteEstadual cndClienteEstadual);


        bool Update(CndClienteEstadual cndClienteEstadual);

        bool Delete(CndClienteEstadual cndClienteEstadual);
        bool Save();
    }
}
