using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewDb;
using PrecisoPRO.Models.ViewModels;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using X.PagedList;

namespace PrecisoPRO.Controllers
{
    public class EmpresaController : Controller
    {
       
        //contexto do banco de dados
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly INatJuridica _natJuridica;
        private readonly IRegimeJuridico _regimeJuridico;

        private readonly IEmpresaViewGeral _empresaViewGeral;

        private static readonly HttpClient httpClient = new HttpClient();

        IEnumerable<Empresa>? listaEmpresas; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados
        IEnumerable<NaturezaJuridica>? listaNatJuridica; //lista as naturezas juridicas       
        IEnumerable<RegimeJuridico>? listaRegimeJuridicos;

        IEnumerable<EmpresaViewGeral>? listaEmpresaViewGeral;
        int contSalvos = 0;
        public EmpresaController(IEmpresaRepository empresaRepository, IEstadoRepository estadoRepository, INatJuridica natJuridica, IRegimeJuridico regJuridico, IEmpresaViewGeral empresaViewGeral)
        {
            _empresaRepository = empresaRepository;
            _estadoRepository = estadoRepository;
            _natJuridica = natJuridica;
            _regimeJuridico = regJuridico;
            _empresaViewGeral = empresaViewGeral;


        }   
        public async Task<IActionResult> Index(string cnpj, string razao, string cidade, string fantasia, string estado, int numPagina = 1) 
        {
            this.listaEmpresas = await _empresaRepository.GetAllAsyncNoTracking();
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();
            if (cnpj != null)
            {
                this.listaEmpresas = this.listaEmpresas.Where(x => x.Cnpj.Contains(cnpj)).ToList();
                ViewBag.Cnpj = cnpj;
            }

            if (razao != null)
            {
                this.listaEmpresas = this.listaEmpresas.Where(x=>x.Razao.Contains(razao)).ToList();
                ViewBag.Razao = razao;
            }
            if(cidade != null)
            {
                this.listaEmpresas = this.listaEmpresas.Where(x => x.Cidade.Contains(cidade)).ToList();
                ViewBag.Cidade = cidade;
            }
            if (fantasia != null)
            {
                this.listaEmpresas = this.listaEmpresas.Where(x => x.Fantasia.Contains(fantasia)).ToList();
                ViewBag.Fantasia = fantasia;
            }
           
           
            if(estado !=null && estado != "")
            {
                this.listaEmpresas = this.listaEmpresas.Where(x => x.UF.Contains(estado)).ToList();
                ViewBag.Estado = estado;
            }
           
           
            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.RegimesJuridicos = this.listaRegimeJuridicos.ToList();
            //passa inicialmente dois parametros, o numero da pagina e o tamanho da página
            return View(this.listaEmpresas.ToPagedList(numPagina, 8));
        }

        //create
        public async Task<IActionResult> Incluir()
        {
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();
            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.NatJuridica = this.listaNatJuridica.ToList();
            ViewBag.RegimeJuridico = this.listaRegimeJuridicos.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Incluir(EmpresaViewModel? empresaVM)
        {

            if (ModelState.IsValid)
            {
               
                var empresa = new Empresa
                {
                    Ie = empresaVM.Ie,
                    Im = empresaVM.Im,
                    Cnpj = empresaVM.Cnpj,
                    Razao = empresaVM.Razao,
                    NMei = empresaVM.NMei,
                    Fantasia = empresaVM.Fantasia,
                    NatJuridica = empresaVM.NatJuridica,
                    RegJuridico = empresaVM.RegJuridico,
                    AtvPrincipal = empresaVM.AtvPrincipal,
                    Cep = empresaVM.Cep,
                    Endereco = empresaVM.Endereco,
                    Numero = empresaVM.Numero,
                    Complemento = empresaVM.Complemento,
                    Bairro = empresaVM.Bairro,
                    Cidade = empresaVM.Cidade,
                    UF = empresaVM.UF,
                    Referencia = empresaVM.Referencia,
                    Principal = empresaVM.Principal,
                    Telefone = empresaVM.Telefone,
                    Celular = empresaVM.Celular,
                    Email = empresaVM.Email,
                    SitCadastral = empresaVM.SitCadastral,
                    MotDescCred = empresaVM.MotDescCred,
                    Nire = empresaVM.Nire,
                    NomeContador = empresaVM.NomeContador,
                    CrcContador = empresaVM.CrcContador,
                    NomeResponsavel = empresaVM.NomeResponsavel,
                    CrcResponsavel = empresaVM.CrcResponsavel,
                    TelAlternativo = empresaVM.TelAlternativo,
                    CelAlternativo = empresaVM.CelAlternativo,
                    EmailAlternativo = empresaVM.EmailAlternativo,
                    LoginDteSefaz = empresaVM.LoginDteSefaz,
                    SenhaDteSefaz = empresaVM.SenhaDteSefaz,
                    CpfRepresentante = empresaVM.CpfRepresentante,
                    Anotacoes = empresaVM.Anotacoes,
                    Data_Cad = DateTime.Now,
                    Data_Alt = DateTime.Now


                };
                try
                {
                    _empresaRepository.Adicionar(empresa);
                    TempData["Success"] = "Registro SALVO com sucesso";
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    TempData["Error"] = "Probelmas ao salvar o registro, tente novamente";
                    return RedirectToAction("Index");
                }
               
                
            }
            else
            {
                ModelState.AddModelError("", "ERRO");
            }
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();
            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.NatJuridica = this.listaNatJuridica.ToList();
            ViewBag.RegimeJuridico = this.listaRegimeJuridicos.ToList();
            return View(empresaVM);
        }

        public async Task<IActionResult>AlterarRegime(int id, string regimeNome)
        {
            //Seleciona a Empresa pelo ID e cria um objeto dela
            Empresa empresa = await _empresaRepository.GetByIdAsync(id);

            //Carrega a lista de Regimejuridico
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();

            //Asssocia o valor do parametro a uma variavel
            string regime = regimeNome;

            //Busca o objeto em forma de var do regime juridico
            var regimeJuridico = this.listaRegimeJuridicos.FirstOrDefault(r => r.Nome == regimeNome);

            //verifica o retorno e associa a variavel idregime
            if(regimeJuridico != null)
            {
                int regimeId = regimeJuridico.Id;
                empresa.RegJuridico = regimeId;
            }

            try
            {
                _empresaRepository.Update(empresa);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Error"] = "Probelmas ao salvar o registro, tente novamente";
                return RedirectToAction("Index");
            }


            ViewBag.RegimesJuridicos = this.listaRegimeJuridicos.ToList();

            return View(empresa);
        }

        [HttpGet]
        public async Task<IActionResult> IncluirLote(int id)
        {
            ViewBag.Ninput = id;
            return View();
        }

   
        public async Task<IActionResult> AdicionarEmpresaLote(string cnpjs)
        {
            this.listaEmpresaViewGeral = await _empresaViewGeral.GetAllAsyncNoTracking();
            this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();

            int tmenor = 0;
            int tcerto = 0;
            int cnpjEncontrado = 0;
            int cnpjErrado = 0;
            int idNatJuridica = 0;
            string atv_principal = null;
            string socioResp = null;
            string telefoneOrganizado = null;
            //receber a string
            List<string> listaCnpjs = cnpjs.Split(',').ToList();

            //classe que representa o retorno da apiexterna
            //List<dynamic> resultados = new List<dynamic>();
            List<Dictionary<string, object>> resultados = new List<Dictionary<string, object>>();

            foreach (var cnpj in listaCnpjs)
            {
                if(cnpj.Count() > 14)
                {
                    tmenor++;
                }
                else
                {
                    //aqui ele vai chamar a api para gravar os dados
                    // Construa a URL com base no CNPJ
                    string apiUrl = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                    // Faça a chamada GET para a API externa
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verifique se a chamada foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {

                        string conteudo = await response.Content.ReadAsStringAsync();

                        // Use JsonDocument para analisar o JSON dinamicamente
                        using (JsonDocument document = JsonDocument.Parse(conteudo))
                        {
                            Dictionary<string, object> resultado = new Dictionary<string, object>();

                            foreach (JsonProperty property in document.RootElement.EnumerateObject())
                            {
                                resultado.Add(property.Name, property.Value.ToString());
                            }

                            // Adicione o resultado à lista (cada empresa sera colocada nessa lista)
                            resultados.Add(resultado);
                        }

                        

                    }
                    else
                    {
                        //Se for 404 soma em uma
                        cnpjErrado++;
                    }

                   
                }
            }//fim foreach


            //neste ponto eu vou ter a lista com todos os cnpj e todos os valores
            //agora vamos varrer a lista (dicionario resultados) em busca de cnpjs que ja estejam cadastrados
            for(int i=0; i<(resultados.Count()); i++)
            {
                //aqui ele vai verificar se o status de cada um dos elementos esta válido
                if (resultados[i]["status"].Equals("ERROR"))
                {
                    
                    cnpjErrado++;
                }
                else
                {
                    //pegar o responsavel ou socio
                    string inputStringSocio = resultados[i]["qsa"].ToString();

                    //encontrar a posição
                    int startIndexSocio = inputStringSocio.IndexOf("\"nome\":");

                    if(startIndexSocio != -1)
                    {
                        // Encontrar a posição inicial da string do valor de "text"
                        int startValueIndexSocio = inputStringSocio.IndexOf("\"", startIndexSocio + 7);
                        if(startValueIndexSocio != -1)
                        {
                            // Encontrar a posição final da string do valor de "text"
                            int endValueIndexSocio = inputStringSocio.IndexOf("\"", startValueIndexSocio + 1);
                            if (endValueIndexSocio != -1)
                            {
                                // Extrair a substring correspondente ao valor de "text"
                                socioResp = inputStringSocio.Substring(startValueIndexSocio + 1, endValueIndexSocio - startValueIndexSocio - 1);


                            }
                        }
                    }


                    //Pegar a atividade principal
                    string inputString = resultados[i]["atividade_principal"].ToString();

                    // Encontrar a posição inicial da string "text"
                    int startIndex = inputString.IndexOf("\"text\":");

                    if (startIndex != -1)
                    {
                        // Encontrar a posição inicial da string do valor de "text"
                        int startValueIndex = inputString.IndexOf("\"", startIndex + 7);

                        if (startValueIndex != -1)
                        {
                            // Encontrar a posição final da string do valor de "text"
                            int endValueIndex = inputString.IndexOf("\"", startValueIndex + 1);

                            if (endValueIndex != -1)
                            {
                                // Extrair a substring correspondente ao valor de "text"
                                atv_principal = inputString.Substring(startValueIndex + 1, endValueIndex - startValueIndex - 1);

                                
                            }
                        }
                    }
                    // fim pegar atividade principal

                    //ytratar a string telefone
                    string telefone = resultados[i]["telefone"].ToString();

                    // Remover espaços em branco
                    string semEspacos = telefone.Replace(" ", "");

                    // Pegar as 13 primeiras posições
                    telefoneOrganizado = semEspacos.Length >= 13 ? semEspacos.Substring(0, 13) : semEspacos;



                    //pegar a natureza juridica e buscar na tabela seu código no banco
                    string natJuridicaString = resultados[i]["natureza_juridica"].ToString();
                    string pattern = @"(\d+-\d+)";
                    Match match = Regex.Match(natJuridicaString, pattern);


                    if (match.Success)
                    {
                        // Acessar o valor correspondente ao padrão na expressão regular
                         natJuridicaString = match.Groups[1].Value;

                        
                    }
                    //agora precisamos buscar esse codigo dentro da tabela mysql
                    NaturezaJuridica? naturezaJuridica = listaNatJuridica.FirstOrDefault(x => x.Codigo.Equals(natJuridicaString));

                    if (naturezaJuridica != null)
                    {
                        idNatJuridica = naturezaJuridica.Id;
                    }
                    string cnpjBusca = resultados[i]["cnpj"].ToString();

                    EmpresaViewGeral? empresaViewGeral = this.listaEmpresaViewGeral.FirstOrDefault(x => x.Cnpj.Equals(cnpjBusca));

                    if (empresaViewGeral != null)
                    {
                        
                        cnpjEncontrado++; //soma um a variavel que teve o cnpjencontrado
                    }
                    else
                    {
                        //aqui vai montar o objeto para salvar
                        var empresa = new Empresa
                        {
                        
                            Cnpj = resultados[i]["cnpj"].ToString(),
                            Razao = resultados[i]["nome"].ToString(),
                            Fantasia =resultados[i]["fantasia"].ToString(),
                            NatJuridica = (idNatJuridica==0)?null:idNatJuridica,
                            AtvPrincipal = (atv_principal==null)?null: atv_principal,
                            Cep = resultados[i]["cep"].ToString(),
                            Endereco = resultados[i]["logradouro"].ToString(),
                            Numero = resultados[i]["numero"].ToString(),
                            Complemento = resultados[i]["complemento"].ToString(),
                            Bairro = resultados[i]["bairro"].ToString(),
                            Cidade = resultados[i]["municipio"].ToString(),
                            UF = resultados[i]["uf"].ToString(),
                            NomeResponsavel = socioResp,
                            Principal = 0,
                            Telefone = telefoneOrganizado,
                            Email = resultados[i]["email"].ToString(),
                            SitCadastral = resultados[i]["situacao"].ToString(),
                            MotDescCred = resultados[i]["motivo_situacao"].ToString(),
                            DataAbertura = resultados[i]["abertura"].ToString(),
                            Data_Cad = DateTime.Now,
                            Data_Alt = DateTime.Now


                        };
                        try 
                        {
                            _empresaRepository.Adicionar(empresa);
                            tcerto++;
                        }
                        catch(Exception e)
                        {
                            cnpjErrado++;
                        }

                        
                    }
                }
               
            }

            if (tcerto > 0)
            {
                if (cnpjErrado > 0)
                {
                    TempData["Success"] = "Cnpjs Salvos: " + tcerto.ToString();
                    TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString();
                }
                else 
                {
                    TempData["Success"] = "Cnpjs Salvos: " + tcerto.ToString();
                }


            }
            else
            {
                if(cnpjErrado > 0)
                {
                    TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString();
                }
              
            }
            if(cnpjEncontrado >0)
            {
                TempData["Info"] = "Cnpjs já cadastrados: " + cnpjEncontrado.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detalhes()
        {
            this.listaEmpresas = await _empresaRepository.GetAll();
            return PartialView();
        }


    }
}

