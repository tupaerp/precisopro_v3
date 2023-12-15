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

         HttpClient httpClient = new HttpClient();

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
            //VARIÁVEIS DE CONTROLE
            int tmenor = 0;
            int tcerto = 0;
            int cnpjEncontrado = 0;
            int cnpjNaoEncontrado = 0;
            int cnpjErrado = 0;
            int idNatJuridica = 0;
            string atv_principal = null;
            string socioResp = null;
            string telefoneOrganizado = null;
            string listaString = null;
            int problemasAoSalvar = 0;
            //Objeto que representa o retorno da apiexterna
            List<Dictionary<string, object>> resultados = new List<Dictionary<string, object>>();
            if (cnpjs != null) {
                //LISTAS PARA VERIFICAÇÕES
                this.listaEmpresaViewGeral = await _empresaViewGeral.GetAllAsyncNoTracking();
                this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();

                //receber a string
                List<string> listaCnpjs = cnpjs.Split(',').ToList();

                

                //FOREACH PARA VARRER A LISTA DE CNPJS ENVIADA DO JS RETIRADA DA MODAL (INPUTS)
                foreach(var cnpj in listaCnpjs)
                {
                    if (string.IsNullOrEmpty(cnpj) || cnpj.Length == 14)
                    {
                        //Formata o cnpj para busca
                        string cnpjForm = FormatarCNPJ(cnpj);
                        //para cada cnpj eu preciso verifiar se existe no banco: Um objeto do modelo especificado
                        // vai receber um registro vindo do banco, se possuir o registro ele pula para o proximo, caso contrario segue o fluxo de execução
                        EmpresaViewGeral? empresaViewGeral = this.listaEmpresaViewGeral.FirstOrDefault(x => x.Cnpj.Equals(cnpjForm));
                        if (empresaViewGeral == null)
                        {
                            //Se ele nao foi encontrado na base, entao precisamos cadastra-lo. A primeira coisa é pegar os dados via API
                            // Construir a URL com base no CNPJ (para cada ncpj fornecido)
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
                                //Se der ERRO
                                cnpjErrado++;
                            }


                        }
                        else
                        {
                            //encontrado - ja cadastrado
                            cnpjEncontrado++;
                        }
                    }
                    else
                    {
                        //se tiver menor que 14 ele soma
                        tmenor++;
                    }
                    
                   


                }//fim for each para cada cnpj
                

            }//fim if cnpj diferente de null
            else
            {
                TempData["Error"] = "Os campos vieram em branco";
                return RedirectToAction("Index");
            }


            //neste ponto eu vou ter a lista com todos os cnpj e todos os valores
            //vamos varrer o dicionario e continuar o fluxo
            for(int i=0; i<(resultados.Count()); i++)
            {
                //verificar se deu alguma responsa ERROR
                if (resultados[i]["status"].Equals("ERROR"))
                {
                    //SOMA MAIS UM À VARIAVEL DE CONTROLE
                    cnpjErrado++;
                }
                else
                {
                    //pegar o responsavel ou socio
                    //manda para a funcao e retorna o  nome do socio
                    string socioRespEmpresa = BuscaSocioRet(resultados[i]["qsa"].ToString());


                
                    //tratar a string telefone
                    string telefone = resultados[i]["telefone"].ToString();

                    // TRATAR STRING TELEFONE
                    string semEspacos = telefone.Replace(" ", "");

                    // Pegar as 13 primeiras posições
                    telefoneOrganizado = semEspacos.Length >= 13 ? semEspacos.Substring(0, 13) : semEspacos;
                    //FIM TRATAR STRING TELEFONE

                    //PEGAR A NATUREZA JURIDICA RETORNADA DA RESPOSTA DA API E BUSCAR O ID NO BANCO
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

                    //SE VIER NULO, PEGA O ID E COLOCA NA VARIAVEL DE APOIO, ELA SERA USADA PARA COMPOR O OBJETO A SER SALVO NO BANCO
                    if (naturezaJuridica != null)
                    {
                        idNatJuridica = naturezaJuridica.Id;
                    }

                    //MONTAR O OBJETO PARA SER SALVO
                    var empresa = new Empresa
                    {

                        Cnpj = resultados[i]["cnpj"].ToString(),
                        Razao = resultados[i]["nome"].ToString(),
                        Fantasia = resultados[i]["fantasia"].ToString(),
                        NatJuridica = (idNatJuridica == 0) ? null : idNatJuridica,
                        AtvPrincipal = (atv_principal == null) ? null : atv_principal,
                        Cep = resultados[i]["cep"].ToString(),
                        Endereco = resultados[i]["logradouro"].ToString(),
                        Numero = resultados[i]["numero"].ToString(),
                        Complemento = resultados[i]["complemento"].ToString(),
                        Bairro = resultados[i]["bairro"].ToString(),
                        Cidade = resultados[i]["municipio"].ToString(),
                        UF = resultados[i]["uf"].ToString(),
                        NomeResponsavel = socioRespEmpresa,
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
                    catch (Exception e)
                    {
                        problemasAoSalvar++;
                    }

                }
            }//FIM DO FOR QUE VARRE O DICIONARIO



            ///*VERIFICA AS VARIÁVEIS DE CONTROLE PARA RETORNAR À INDEX CADA ESTADO DO CADASTRO*/
            if (tcerto > 0)
            {
                if(problemasAoSalvar > 0)
                {
                    if (cnpjErrado > 0)
                    {
                        TempData["Success"] = "Cnpjs Salvos: " + tcerto.ToString();
                        TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString() + " - Não salvo: "+problemasAoSalvar.ToString();
                    }
                    else
                    {
                        TempData["Error"] = "Não salvo: " + problemasAoSalvar.ToString();

                        TempData["Success"] = "Cnpjs Salvos: " + tcerto.ToString();
                    }
                }
                else
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
                


            }
            else
            {
                if (problemasAoSalvar > 0)
                {
                    if (cnpjErrado > 0)
                    {
                        TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString() + " - Não salvo: " + problemasAoSalvar.ToString();
                    }
                    else
                    {
                        TempData["Error"] = "Não salvo: " + problemasAoSalvar.ToString();

                    }
                }
                else
                {
                    if (cnpjErrado > 0)
                    {
                        TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString();
                    }
                }
               

            }
            if (cnpjEncontrado > 0)
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

        public string FormatarCNPJ(string cnpj)
        {
           
            // Formatar o CNPJ
            string cnpjFormatado = $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12, 2)}";

            // Retornar o CNPJ formatado
            return cnpjFormatado;
        }

        //Action para buscar o Socio da empresa no retorno da api
        public string BuscaSocioRet(string inputStringSocio)
        {
            string socioResp = null;
            //encontrar a posição
            int startIndexSocio = inputStringSocio.IndexOf("\"nome\":");
            if (startIndexSocio != -1)
            {
                // Encontrar a posição inicial da string do valor de "text"
                int startValueIndexSocio = inputStringSocio.IndexOf("\"", startIndexSocio + 7);
                if (startValueIndexSocio != -1)
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
            return socioResp;
        }

    }
}

