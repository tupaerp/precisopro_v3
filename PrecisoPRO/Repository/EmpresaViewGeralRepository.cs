using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models.ViewDb;

namespace PrecisoPRO.Repository
{
    public class EmpresaViewGeralRepository : IEmpresaViewGeral
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public EmpresaViewGeralRepository(PrecisoPRODbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<EmpresaViewGeral>> GetAll()
        {
            //nao precisa do include pois é uma view do banco, ele ja traz tudo
            return await db.EmpresasViewGeral.ToListAsync();
        }

        public async Task<IEnumerable<EmpresaViewGeral>> GetAllAsyncNoTracking()
        {
            return await db.EmpresasViewGeral.AsNoTracking().OrderBy(x => x.IdEmpresa).ToListAsync();
        }

        public async Task<EmpresaViewGeral> GetByCnpjAsync(string cnpj)
        {
            return await db.EmpresasViewGeral.FirstOrDefaultAsync(i => i.Cnpj == cnpj);
        }

        public async Task<EmpresaViewGeral> GetByCnpjAsyncNoTracking(string cnpj)
        {
            return await db.EmpresasViewGeral.AsNoTracking().FirstOrDefaultAsync(i => i.Cnpj == cnpj);
        }

        public async Task<EmpresaViewGeral> GetByIdAsync(int id)
        {
            return await db.EmpresasViewGeral.FirstOrDefaultAsync(i => i.IdEmpresa == id);
        }

        public async Task<EmpresaViewGeral> GetByIdAsyncNoTracking(int id)
        {
            return await db.EmpresasViewGeral.AsNoTracking().FirstOrDefaultAsync(i => i.IdEmpresa == id);
        }
    }
}
