using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Repository;
using PrecisoPRO.Services;
using System.Xml;
using X.PagedList;

namespace PrecisoPRO.Controllers
{
    public class CndEstadualController : Controller
    {
        HttpClient httpClient = new HttpClient();
      
        //Serviço API Estadual
        private readonly SefazToApiService _sefazToApiService;
        private readonly ICndClienteEstadual _cndEmpresaEstadual;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstadoRepository _estadoRepository;

        IEnumerable<Empresa>? listaEmpresas; //Lista enumerada
        IEnumerable<CndClienteEstadual>? listaCndEmpresasEstaduais; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados
        //Construtor
        public CndEstadualController(
            SefazToApiService sefazToApiService, 
            ICndClienteEstadual cndEmpresaEstadual,
            IEmpresaRepository empresaRepository,
            IEstadoRepository estadoRepository
            )
        {
            _sefazToApiService  = sefazToApiService;
            _cndEmpresaEstadual = cndEmpresaEstadual;
            _empresaRepository  = empresaRepository;
            _estadoRepository   = estadoRepository;
           

        }
        public async Task<IActionResult> Index(string cnpj, string razao, string cidade, string fantasia, string estado, int numPagina = 1)
        {
            this.listaEmpresas = await _empresaRepository.GetAllAsyncNoTracking();
            this.listaCndEmpresasEstaduais = await _cndEmpresaEstadual.GetAllAsyncNoTracking();
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();

            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.Empresas = this.listaEmpresas.ToList();
            return View(this.listaCndEmpresasEstaduais.ToPagedList(numPagina, 8));
        }

        //Metodo Get para Abrir a Página que vai receber os dados
        public async Task<IActionResult> ConsultarCnd()
        {
            this.listaEstados  = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaEmpresas = await _empresaRepository.GetAllAsyncNoTracking();
            ViewBag.Empresas   = this.listaEmpresas.ToList();
            ViewBag.Estados    = this.listaEstados.ToList();
            return View();
        }

        
       

    }
}
