using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewDb;

namespace PrecisoPRO.Interfaces
{
    public interface IEmpresaViewGeral
    {
        Task<IEnumerable<EmpresaViewGeral>> GetAll();

        Task<IEnumerable<EmpresaViewGeral>> GetAllAsyncNoTracking();

       

        Task<EmpresaViewGeral> GetByIdAsync(int id);

        Task<EmpresaViewGeral> GetByIdAsyncNoTracking(int id);


        Task<EmpresaViewGeral> GetByCnpjAsync(string cnpj);


        Task<EmpresaViewGeral> GetByCnpjAsyncNoTracking(string cnpj);





    }
}
