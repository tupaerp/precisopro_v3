using PrecisoPRO.Models;

namespace PrecisoPRO.Interfaces
{
    public interface ICndClienteFederal
    {
        Task<IEnumerable<CndClienteFederal>> GetAll();

        Task<CndClienteFederal> GetByIdAsync(int id);

        Task<CndClienteFederal> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<CndClienteFederal>> GetClienteByCity(string uf);

        Task<IEnumerable<CndClienteFederal>> GetAllAsyncNoTracking();

        bool Adicionar(CndClienteFederal cndClienteFederal);


        bool Update(CndClienteFederal cndClienteFederal);

        bool Delete(CndClienteFederal cndClienteFederal);
        bool Save();
    }
}
