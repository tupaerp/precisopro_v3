using Microsoft.AspNetCore.Mvc;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Services;
using X.PagedList;

namespace PrecisoPRO.Controllers
{
    public class CndFederalController : Controller
    {
        private readonly ICndClienteFederal _cndClienteFederal;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstadoRepository _estadoRepository;

        IEnumerable<Cliente>? listaClientes; //Lista enumerada
        IEnumerable<CndClienteFederal>? listaCndClientesFederais; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados


        public CndFederalController(
           ICndClienteFederal cndClienteFederal,
           IClienteRepository clienteRepository,
           IEstadoRepository estadoRepository
           )
        {
            _cndClienteFederal = cndClienteFederal;
            _clienteRepository = clienteRepository;
            _estadoRepository = estadoRepository;
        }
        public async Task<IActionResult> Index(string cnpj, string razao, string cidade, string estado, string status, int numPagina = 1)
        {
            this.listaClientes = await _clienteRepository.GetAllAsyncNoTracking();
            this.listaCndClientesFederais = await _cndClienteFederal.GetAllAsyncNoTracking();
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();

            //TO-DO -> FILTROS

            //Busca os Estados e empresas
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.Clientes = this.listaClientes.ToList();
            return View(this.listaCndClientesFederais.ToPagedList(numPagina, 8));

            
        }

        //Atualizar todas as CNDS DAS EMPRESAS CADASTRADAS
    }
}
