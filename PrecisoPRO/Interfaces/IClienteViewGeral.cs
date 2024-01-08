using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewDb;

namespace PrecisoPRO.Interfaces
{
    public interface IClienteViewGeral
    {
        Task<IEnumerable<ClienteViewGeral>> GetAll();

        Task<IEnumerable<ClienteViewGeral>> GetAllAsyncNoTracking();



        Task<ClienteViewGeral> GetByIdAsync(int id);

        Task<ClienteViewGeral> GetByIdAsyncNoTracking(int id);


        Task<ClienteViewGeral> GetByCnpjAsync(string cnpj);


        Task<ClienteViewGeral> GetByCnpjAsyncNoTracking(string cnpj);





    }
}
