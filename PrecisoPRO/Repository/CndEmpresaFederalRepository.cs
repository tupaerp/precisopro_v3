using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;

namespace PrecisoPRO.Repository
{
    public class CndEmpresaFederalRepository : ICndEmpresaFederal
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public CndEmpresaFederalRepository(PrecisoPRODbContext context)
        {
            db = context;
        }

        public bool Adicionar(CndEmpresaFederal cndEmpresaFederal)
        {
            db.Add(cndEmpresaFederal);
            return Save();
        }

        public bool Delete(CndEmpresaFederal cndEmpresaFederal)
        {
            db.Remove(cndEmpresaFederal);
            return Save();
        }

        public async Task<IEnumerable<CndEmpresaFederal>> GetAll()
        {
            return await db.CndEmpresaFederais.Include(i => i.Empresa).ToListAsync();

        }

        public async Task<IEnumerable<CndEmpresaFederal>> GetAllAsyncNoTracking()
        {
            return await db.CndEmpresaFederais.Include(i => i.Empresa).AsNoTracking().OrderBy(x => x.Id).ToListAsync();

        }

        public async Task<CndEmpresaFederal> GetByIdAsync(int id)
        {
            return await db.CndEmpresaFederais.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<CndEmpresaFederal> GetByIdAsyncNoTracking(int id)
        {
            return await db.CndEmpresaFederais.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CndEmpresaFederal>> GetEmpresaByCity(string uf)
        {
            return await db.CndEmpresaFederais.Where(c => c.Empresa.UF.Contains(uf)).ToListAsync();
        }

        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(CndEmpresaFederal cndEmpresaFederal)
        {
            db.Update(cndEmpresaFederal);
            return Save();
        }
    }
}
