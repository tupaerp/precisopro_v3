using PrecisoPRO.Models;

namespace PrecisoPRO.Interfaces
{
    public interface ICndEmpresaFederal
    {
        Task<IEnumerable<CndEmpresaFederal>> GetAll();

        Task<CndEmpresaFederal> GetByIdAsync(int id);

        Task<CndEmpresaFederal> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<CndEmpresaFederal>> GetEmpresaByCity(string uf);

        Task<IEnumerable<CndEmpresaFederal>> GetAllAsyncNoTracking();

        bool Adicionar(CndEmpresaFederal cndEmpresaFederal);


        bool Update(CndEmpresaFederal cndEmpresaFederal);

        bool Delete(CndEmpresaFederal cndEmpresaFederal);
        bool Save();
    }
}
