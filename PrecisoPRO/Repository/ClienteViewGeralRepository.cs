using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models.ViewDb;

namespace PrecisoPRO.Repository
{
    public class ClienteViewGeralRepository : IClienteViewGeral
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public ClienteViewGeralRepository(PrecisoPRODbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<ClienteViewGeral>> GetAll()
        {
            //nao precisa do include pois é uma view do banco, ele ja traz tudo
            return await db.ClientesViewGeral.ToListAsync();
        }

        public async Task<IEnumerable<ClienteViewGeral>> GetAllAsyncNoTracking()
        {
            return await db.ClientesViewGeral.AsNoTracking().OrderBy(x => x.IdCliente).ToListAsync();
        }

        public async Task<ClienteViewGeral> GetByCnpjAsync(string cnpj)
        {
            return await db.ClientesViewGeral.FirstOrDefaultAsync(i => i.Cnpj == cnpj);
        }

        public async Task<ClienteViewGeral> GetByCnpjAsyncNoTracking(string cnpj)
        {
            return await db.ClientesViewGeral.AsNoTracking().FirstOrDefaultAsync(i => i.Cnpj == cnpj);
        }

        public async Task<ClienteViewGeral> GetByIdAsync(int id)
        {
            return await db.ClientesViewGeral.FirstOrDefaultAsync(i => i.IdCliente == id);
        }

        public async Task<ClienteViewGeral> GetByIdAsyncNoTracking(int id)
        {
            return await db.ClientesViewGeral.AsNoTracking().FirstOrDefaultAsync(i => i.IdCliente == id);
        }
    }
}
