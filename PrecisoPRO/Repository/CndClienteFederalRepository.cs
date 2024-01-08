using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;

namespace PrecisoPRO.Repository
{
    public class CndClienteFederalRepository : ICndClienteFederal
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public CndClienteFederalRepository(PrecisoPRODbContext context)
        {
            db = context;
        }

        public bool Adicionar(CndClienteFederal cndClienteFederal)
        {
            db.Add(cndClienteFederal);
            return Save();
        }

        public bool Delete(CndClienteFederal cndClienteFederal)
        {
            db.Remove(cndClienteFederal);
            return Save();
        }

        public async Task<IEnumerable<CndClienteFederal>> GetAll()
        {
            return await db.CndClientesFederais.Include(i => i.Cliente).ToListAsync();

        }

        public async Task<IEnumerable<CndClienteFederal>> GetAllAsyncNoTracking()
        {
            return await db.CndClientesFederais.Include(i => i.Cliente).AsNoTracking().OrderBy(x => x.Id).ToListAsync();

        }

        public async Task<CndClienteFederal> GetByIdAsync(int id)
        {
            return await db.CndClientesFederais.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<CndClienteFederal> GetByIdAsyncNoTracking(int id)
        {
            return await db.CndClientesFederais.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CndClienteFederal>> GetClienteByCity(string uf)
        {
            return await db.CndClientesFederais.Where(c => c.Cliente.UF.Contains(uf)).ToListAsync();
        }

        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(CndClienteFederal cndClienteFederal)
        {
            db.Update(cndClienteFederal);
            return Save();
        }
    }
}
