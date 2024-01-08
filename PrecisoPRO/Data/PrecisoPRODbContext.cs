using Microsoft.EntityFrameworkCore;
using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewDb;
using PrecisoPRO.Models.ViewModels;
using System.Diagnostics;

namespace PrecisoPRO.Data
{
    public class PrecisoPRODbContext : DbContext
    {
       
      
        public PrecisoPRODbContext(DbContextOptions<PrecisoPRODbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Estado> Estados { get; set; }

       public DbSet<AssociarEmpUf> AssociarEmpresasUf { get; set; }

        public DbSet<NaturezaJuridica> NatJuridicas { get; set; }

        public DbSet<RegimeJuridico> RegimeJuridicos { get; set; }

        public DbSet<EmpresaViewGeral> EmpresasViewGeral { get; set; }

        public DbSet<ClienteViewGeral> ClientesViewGeral { get; set; }

        public DbSet<CndClienteEstadual> CndClientesEstaduais { get; set; }

        public DbSet<CndClienteFederal> CndClientesFederais { get; set; }



    }
}
