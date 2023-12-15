using Microsoft.AspNetCore.Mvc;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewModels;

namespace PrecisoPRO.Controllers
{
    public class BuscarcndsController : Controller
    {
        //contexto do banco de dados
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstadoRepository _estadoRepository;
 
        IEnumerable<Empresa>? listaEmpresas; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados

        IEnumerable<AssociarEmpUf>? listaAssociarEmpUF;
        int contSalvos = 0;

        //CONSTRUTOR
        public BuscarcndsController(IEmpresaRepository empresaRepository, IEstadoRepository estadoRepository)
        {
            _empresaRepository = empresaRepository;
            _estadoRepository = estadoRepository;
           
           
        }
        public async Task<IActionResult> Index(string cnpj, string ie, string  finalidade="CADASTRO")
        {
            

            return View();
        }

        


      
    }
}
