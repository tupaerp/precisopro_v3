using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Data;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;

namespace PrecisoPRO.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //contexto do banco de dados
        private readonly PrecisoPRODbContext db;

        public ClienteRepository(PrecisoPRODbContext context)
        {
            db = context;
        }


        public bool Adicionar(Cliente cliente)
        {
            db.Add(cliente);
            return Save();
        }

        public bool Delete(Cliente cliente)
        {
            db.Remove(cliente);
            return Save();
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await db.Clientes.Include(i => i.RegimeJuridico).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetAllAsyncNoTracking()
        {
            //return await db.Empresas.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            return await db.Clientes.Include(i => i.RegimeJuridico).AsNoTracking().OrderBy(x => x.Id).ToListAsync();

        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await db.Clientes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Cliente> GetByIdAsyncNoTracking(int id)
        {
            return await db.Clientes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IEnumerable<Cliente>> GetEmpresaByCity(string cidade)
        {
            return await db.Clientes.Where(c => c.Cidade.Contains(cidade)).ToListAsync();
        }

        public bool Save()
        {

            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(Cliente cliente)
        {
            db.Update(cliente);
            return Save();
        }
    }
}
