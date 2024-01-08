using Microsoft.AspNetCore.Mvc;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Services;
using X.PagedList;

namespace PrecisoPRO.Controllers
{
    public class CndFederalController : Controller
    {
        private readonly ICndEmpresaFederal _cndEmpresaFederal;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstadoRepository _estadoRepository;

        IEnumerable<Empresa>? listaEmpresas; //Lista enumerada
        IEnumerable<CndEmpresaFederal>? listaCndEmpresasFederais; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados


        public CndFederalController(
           ICndEmpresaFederal cndEmpresaFederal,
           IEmpresaRepository empresaRepository,
           IEstadoRepository estadoRepository
           )
        {
            _cndEmpresaFederal = cndEmpresaFederal;
            _empresaRepository = empresaRepository;
            _estadoRepository = estadoRepository;
        }
        public async Task<IActionResult> Index(string cnpj, string razao, string cidade, string estado, string status, int numPagina = 1)
        {
            this.listaEmpresas = await _empresaRepository.GetAllAsyncNoTracking();
            this.listaCndEmpresasFederais = await _cndEmpresaFederal.GetAllAsyncNoTracking();
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();

            //TO-DO -> FILTROS

            //Busca os Estados e empresas
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.Empresas = this.listaEmpresas.ToList();
            return View(this.listaCndEmpresasFederais.ToPagedList(numPagina, 8));

            
        }

        //Atualizar todas as CNDS DAS EMPRESAS CADASTRADAS
    }
}
