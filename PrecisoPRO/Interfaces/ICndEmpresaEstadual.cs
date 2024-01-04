using PrecisoPRO.Models;

namespace PrecisoPRO.Interfaces
{
    public interface ICndEmpresaEstadual
    {
        Task<IEnumerable<CndEmpresaEstadual>> GetAll();

        Task<CndEmpresaEstadual> GetByIdAsync(int id);

        Task<CndEmpresaEstadual> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<CndEmpresaEstadual>> GetEmpresaByCity(string uf);

        Task<IEnumerable<CndEmpresaEstadual>> GetAllAsyncNoTracking();

        bool Adicionar(CndEmpresaEstadual cndEmpresaEstadual);


        bool Update(CndEmpresaEstadual cndEmpresaEstadual);

        bool Delete(CndEmpresaEstadual cndEmpresaEstadual);
        bool Save();
    }
}
