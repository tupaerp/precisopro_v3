using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;

namespace PrecisoPRO.Repository
{
    public class CndEmpresaEstadualRepository : ICndEmpresaEstadual
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public CndEmpresaEstadualRepository(PrecisoPRODbContext context)
        {
            db = context;
        }


        public bool Adicionar(CndEmpresaEstadual cndEmpresaEstadual)
        {
            db.Add(cndEmpresaEstadual);
            return Save();
        }

        public bool Delete(CndEmpresaEstadual cndEmpresaEstadual)
        {
            db.Remove(cndEmpresaEstadual);
            return Save();
        }

        public async Task<IEnumerable<CndEmpresaEstadual>> GetAll()
        {
            return await db.CndEmpresaEstaduais.Include(i => i.Empresa).ToListAsync();
        }

        public async Task<IEnumerable<CndEmpresaEstadual>> GetAllAsyncNoTracking()
        {
            return await db.CndEmpresaEstaduais.Include(i => i.Empresa).AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<CndEmpresaEstadual> GetByIdAsync(int id)
        {
            return await db.CndEmpresaEstaduais.FirstOrDefaultAsync(i => i.Id == id);
        }

     

        public async Task<CndEmpresaEstadual> GetByIdAsyncNoTracking(int id)
        {
            return await db.CndEmpresaEstaduais.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CndEmpresaEstadual>> GetEmpresaByCity(string uf)
        {
            return await db.CndEmpresaEstaduais.Where(c => c.Uf.Contains(uf)).ToListAsync();
        }

        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(CndEmpresaEstadual cndEmpresaEstadual)
        {
            db.Update(cndEmpresaEstadual);
            return Save();
        }
    }
}
