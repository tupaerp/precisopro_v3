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
        private readonly ICndEmpresaEstadual _cndEmpresaEstadual;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstadoRepository _estadoRepository;

        IEnumerable<Empresa>? listaEmpresas; //Lista enumerada
        IEnumerable<CndEmpresaEstadual>? listaCndEmpresasEstaduais; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados
        //Construtor
        public CndEstadualController(
            SefazToApiService sefazToApiService, 
            ICndEmpresaEstadual cndEmpresaEstadual,
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

        //Metodo Get para Abrir a Página que vai receber os dados
        [HttpPost]
        public async Task<IActionResult> IncluirCnd(string? cnpj, List<string> checkUf)
        {
            

            //forech para buscar cada CND de seu estado
            foreach(var estado in checkUf)
            {
                //switch case para estado - SELECIONA O ESTADO
                switch (estado)
                {
                    case "AC":
                        Console.WriteLine("Acre - Região Norte");
                        break;

                    case "AL":
                        Console.WriteLine("Alagoas - Região Nordeste");
                        break;

                    case "AP":
                        Console.WriteLine("Amapá - Região Norte");
                        break;

                    case "AM":
                        Console.WriteLine("Amazonas - Região Norte");
                        break;

                    case "BA":
                        Console.WriteLine("Bahia - Região Nordeste");
                        break;

                    case "CE":
                        Console.WriteLine("Ceará - Região Nordeste");
                        break;

                    case "DF":
                        Console.WriteLine("Distrito Federal - Região Centro-Oeste");
                        break;

                    case "ES":
                        Console.WriteLine("Espírito Santo - Região Sudeste");
                        break;

                    case "GO":
                        Console.WriteLine("Goiás - Região Centro-Oeste");
                        break;

                    case "MA":
                        Console.WriteLine("Maranhão - Região Nordeste");
                        break;

                    case "MT":
                        Console.WriteLine("Mato Grosso - Região Centro-Oeste");
                        break;

                    case "MS":
                        Console.WriteLine("Mato Grosso do Sul - Região Centro-Oeste");
                        break;

                    case "MG":
                        Console.WriteLine("Minas Gerais - Região Sudeste");
                        break;

                    case "PA":
                        Console.WriteLine("Pará - Região Norte");
                        break;

                    case "PB":
                        Console.WriteLine("Paraíba - Região Nordeste");
                        break;

                    case "PR":
                        Console.WriteLine("Paraná - Região Sul");
                        break;

                    case "PE":
                        Console.WriteLine("Pernambuco - Região Nordeste");
                        break;

                    case "PI":
                        Console.WriteLine("Piauí - Região Nordeste");
                        break;

                    case "RJ":
                        Console.WriteLine("Rio de Janeiro - Região Sudeste");
                        break;

                    case "RN":
                        Console.WriteLine("Rio Grande do Norte - Região Nordeste");
                        break;

                    case "RS":
                        Console.WriteLine("Rio Grande do Sul - Região Sul");
                        break;

                    case "RO":
                        Console.WriteLine("Rondônia - Região Norte");
                        break;

                    case "RR":
                        Console.WriteLine("Roraima - Região Norte");
                        break;

                    case "SC":
                        Console.WriteLine("Santa Catarina - Região Sul");
                        break;

                    case "SP":
                        Console.WriteLine("São Paulo - Região Sudeste");
                        break;

                    case "SE":
                        Console.WriteLine("Sergipe - Região Nordeste");
                        break;

                    case "TO":
                        try
                        {
                           


                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Erro ao consultar a API: {ex.Message}");
                        }
                        break;

                    default:
                        Console.WriteLine("Sigla de estado não reconhecida.");
                        break;

                }
            }

            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaEmpresas = await _empresaRepository.GetAllAsyncNoTracking();
            ViewBag.Empresas = this.listaEmpresas.ToList();
            ViewBag.Estados = this.listaEstados.ToList();
            return RedirectToAction("CndEstadual");
        }
       

    }
}
