using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;

namespace PrecisoPRO.Repository
{
    public class CndClienteEstadualRepository : ICndClienteEstadual
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public CndClienteEstadualRepository(PrecisoPRODbContext context)
        {
            db = context;
        }


        public bool Adicionar(CndClienteEstadual cndClienteEstadual)
        {
            db.Add(cndClienteEstadual);
            return Save();
        }

        public bool Delete(CndClienteEstadual cndClienteEstadual)
        {
            db.Remove(cndClienteEstadual);
            return Save();
        }

        public async Task<IEnumerable<CndClienteEstadual>> GetAll()
        {
            return await db.CndClientesEstaduais.Include(i => i.Cliente).ToListAsync();
        }

        public async Task<IEnumerable<CndClienteEstadual>> GetAllAsyncNoTracking()
        {
            return await db.CndClientesEstaduais.Include(i => i.Cliente).AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<CndClienteEstadual> GetByIdAsync(int id)
        {
            return await db.CndClientesEstaduais.FirstOrDefaultAsync(i => i.Id == id);
        }

     

        public async Task<CndClienteEstadual> GetByIdAsyncNoTracking(int id)
        {
            return await db.CndClientesEstaduais.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CndClienteEstadual>> GetClienteByCity(string uf)
        {
            return await db.CndClientesEstaduais.Where(c => c.Uf.Contains(uf)).ToListAsync();
        }

        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(CndClienteEstadual cndClienteEstadual)
        {
            db.Update(cndClienteEstadual);
            return Save();
        }
    }
}
