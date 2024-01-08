using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using PrecisoPRO.Interfaces;
using PrecisoPRO.Models;
using PrecisoPRO.Models.ViewDb;
using PrecisoPRO.Models.ViewModels;
using PrecisoPRO.Services;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using X.PagedList;

namespace PrecisoPRO.Controllers
{
    public class ClienteController : Controller
    {
        //contexto do banco de dados
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly INatJuridica _natJuridica;
        private readonly IRegimeJuridico _regimeJuridico;

        private readonly IClienteViewGeral _clienteViewGeral;

        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;

        IEnumerable<Cliente>? listaClientes; //Lista enumerada
        IEnumerable<Estado>? listaEstados; //lista de estados
        IEnumerable<NaturezaJuridica>? listaNatJuridica; //lista as naturezas juridicas       
        IEnumerable<RegimeJuridico>? listaRegimeJuridicos;

        IEnumerable<ClienteViewGeral>? listaClienteViewGeral;


        
        int contSalvos = 0;
        public ClienteController(
            IClienteRepository clienteRepository,
            IEstadoRepository estadoRepository,
            INatJuridica natJuridica,
            IRegimeJuridico regJuridico,
            IClienteViewGeral clienteViewGeral
            )
        {
            _clienteRepository = clienteRepository;
            _estadoRepository = estadoRepository;
            _natJuridica = natJuridica;
            _regimeJuridico = regJuridico;
            _clienteViewGeral = clienteViewGeral;



        }
        public async Task<IActionResult> Index(string cnpj, string razao, string cidade, string fantasia, string estado, int numPagina = 1)
        {
            this.listaClientes = await _clienteRepository.GetAllAsyncNoTracking();
           
            //Listar somente os clientes referente a empresa do usuário
            //PEGAR OS DADOS DO USUÁRIO DA SESSÃO
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);


            this.listaClientes = this.listaClientes.Where(x => x.EmpresaId.Equals(usuario.EmpresaId)).ToList();


            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();

            if (cnpj != null)
            {
                this.listaClientes = this.listaClientes.Where(x => x.Cnpj.Contains(cnpj)).ToList();
                ViewBag.Cnpj = cnpj;
            }

            if (razao != null)
            {
                this.listaClientes = this.listaClientes.Where(x => x.Razao.Contains(razao)).ToList();
                ViewBag.Razao = razao;
            }
            if (cidade != null)
            {
                this.listaClientes = this.listaClientes.Where(x => x.Cidade.Contains(cidade)).ToList();
                ViewBag.Cidade = cidade;
            }
            if (fantasia != null)
            {
                this.listaClientes = this.listaClientes.Where(x => x.Fantasia.Contains(fantasia)).ToList();
                ViewBag.Fantasia = fantasia;
            }


            if (estado != null && estado != "")
            {
                this.listaClientes = this.listaClientes.Where(x => x.UF.Contains(estado)).ToList();
                ViewBag.Estado = estado;
            }


            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.RegimesJuridicos = this.listaRegimeJuridicos.ToList();
            //passa inicialmente dois parametros, o numero da pagina e o tamanho da página
            return View(this.listaClientes.ToPagedList(numPagina, 8));
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
        public async Task<IActionResult> Incluir(ClienteViewModel? clienteVM)
        {
            //PEGAR OS DADOS DO USUÁRIO DA SESSÃO
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

            this.listaClienteViewGeral = await _clienteViewGeral.GetAllAsyncNoTracking();



            // vai receber um registro vindo do banco, se possuir o registro ele pula para o proximo, caso contrario segue o fluxo de execução
            this.listaClienteViewGeral = this.listaClienteViewGeral.Where(x => x.Cnpj.Equals(clienteVM.Cnpj.ToString()) && x.EmpresaId.Equals(usuario.EmpresaId.ToString())).ToList();

            if (this.listaClienteViewGeral == null || this.listaClienteViewGeral.Count() == 0)
            {
                if (ModelState.IsValid)
                {

                    var cliente = new Cliente
                    {
                        Ie = clienteVM.Ie,
                        Im = clienteVM.Im,
                        Cnpj = clienteVM.Cnpj,
                        Razao = clienteVM.Razao,
                        NMei = clienteVM.NMei,
                        Fantasia = clienteVM.Fantasia,
                        NatJuridica = clienteVM.NatJuridica,
                        RegJuridico = clienteVM.RegJuridico,
                        AtvPrincipal = clienteVM.AtvPrincipal,
                        Cep = clienteVM.Cep,
                        Endereco = clienteVM.Endereco,
                        Numero = clienteVM.Numero,
                        Complemento = clienteVM.Complemento,
                        Bairro = clienteVM.Bairro,
                        Cidade = clienteVM.Cidade,
                        UF = clienteVM.UF,
                        Referencia = clienteVM.Referencia,
                        Telefone = clienteVM.Telefone,
                        Celular = clienteVM.Celular,
                        Email = clienteVM.Email,
                        SitCadastral = clienteVM.SitCadastral,
                        MotDescCred = clienteVM.MotDescCred,
                        Nire = clienteVM.Nire,
                        NomeContador = clienteVM.NomeContador,
                        CrcContador = clienteVM.CrcContador,
                        NomeResponsavel = clienteVM.NomeResponsavel,
                        CrcResponsavel = clienteVM.CrcResponsavel,
                        TelAlternativo = clienteVM.TelAlternativo,
                        CelAlternativo = clienteVM.CelAlternativo,
                        EmailAlternativo = clienteVM.EmailAlternativo,
                        LoginDteSefaz = clienteVM.LoginDteSefaz,
                        SenhaDteSefaz = clienteVM.SenhaDteSefaz,
                        CpfRepresentante = clienteVM.CpfRepresentante,
                        Anotacoes = clienteVM.Anotacoes,
                        Data_Cad = DateTime.Now,
                        Data_Alt = DateTime.Now


                    };
                    try
                    {
                        _clienteRepository.Adicionar(cliente);
                        TempData["Success"] = "Registro SALVO com sucesso";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Error"] = "Probelmas ao salvar o registro, tente novamente";
                        return RedirectToAction("Index");
                    }


                }
                else
                {
                    ModelState.AddModelError("", "ERRO");
                }

            }
            else
            {
                TempData["Info"] = "Cnpj já cadastrado: " + clienteVM.Cnpj.ToString();
                return RedirectToAction("Index");
            }


           
            this.listaEstados = await _estadoRepository.GetAllAsyncNoTracking();
            this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();
            //Busca os Estados
            ViewBag.Estados = this.listaEstados.ToList();
            ViewBag.NatJuridica = this.listaNatJuridica.ToList();
            ViewBag.RegimeJuridico = this.listaRegimeJuridicos.ToList();
            return View(clienteVM);
        }

        public async Task<IActionResult> AlterarRegime(int id, string regimeNome)
        {
            //Seleciona a Empresa pelo ID e cria um objeto dela
            Cliente cliente = await _clienteRepository.GetByIdAsync(id);

            //Carrega a lista de Regimejuridico
            this.listaRegimeJuridicos = await _regimeJuridico.GetAllAsyncNoTracking();

            //Asssocia o valor do parametro a uma variavel
            string regime = regimeNome;

            //Busca o objeto em forma de var do regime juridico
            var regimeJuridico = this.listaRegimeJuridicos.FirstOrDefault(r => r.Nome == regimeNome);

            //verifica o retorno e associa a variavel idregime
            if (regimeJuridico != null)
            {
                int regimeId = regimeJuridico.Id;
                cliente.RegJuridico = regimeId;
            }

            try
            {
                _clienteRepository.Update(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Error"] = "Probelmas ao salvar o registro, tente novamente";
                return RedirectToAction("Index");
            }


            ViewBag.RegimesJuridicos = this.listaRegimeJuridicos.ToList();

            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> IncluirLote(int id)
        {
            ViewBag.Ninput = id;
            return View();
        }


        public async Task<IActionResult> AdicionarClienteLote(string cnpjs)
        {
            //PEGAR OS DADOS DO USUÁRIO DA SESSÃO
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);


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
            if (cnpjs != null)
            {
                //LISTAS PARA VERIFICAÇÕES
                this.listaClienteViewGeral = await _clienteViewGeral.GetAllAsyncNoTracking();
                this.listaNatJuridica = await _natJuridica.GetAllAsyncNoTracking();

                //receber a string
                List<string> listaCnpjs = cnpjs.Split(',').ToList();



                //FOREACH PARA VARRER A LISTA DE CNPJS ENVIADA DO JS RETIRADA DA MODAL (INPUTS)
                foreach (var cnpj in listaCnpjs)
                {
                    if (string.IsNullOrEmpty(cnpj) || cnpj.Length == 14)
                    {
                        //Formata o cnpj para busca
                        string cnpjForm = FormatarCNPJ(cnpj);
                        //para cada cnpj eu preciso verifiar se existe no banco: Um objeto do modelo especificado

                        // vai receber um registro vindo do banco, se possuir o registro ele pula para o proximo, caso contrario segue o fluxo de execução
                        this.listaClienteViewGeral = this.listaClienteViewGeral.Where(x => x.Cnpj.Equals(cnpjForm) && x.EmpresaId.Equals(usuario.EmpresaId.ToString())).ToList();
                        
                        if (this.listaClienteViewGeral == null || this.listaClienteViewGeral.Count() == 0)
                        {
                            //Se ele nao foi encontrado na base, entao precisamos cadastra-lo. A primeira coisa é pegar os dados via API
                            // Construir a URL com base no CNPJ (para cada ncpj fornecido)
                            string apiUrl = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                            // Faça a chamada GET para a API externa
                            //HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                                                       

                            do
                            {
                                response = await httpClient.GetAsync(apiUrl);

                                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                                {
                                    Console.WriteLine("Received 429 - Too Many Requests. Waiting and retrying...");

                                    // Aguardar um curto período de tempo antes de tentar novamente
                                    await Task.Delay(1000); // Aguardar 1 segundo 
                                }

                            } while (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests);



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
                                //Se der ERRO verificar se é status code 429
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
            for (int i = 0; i < (resultados.Count()); i++)
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
                    var cliente = new Cliente
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
                        Telefone = telefoneOrganizado,
                        Email = resultados[i]["email"].ToString(),
                        SitCadastral = resultados[i]["situacao"].ToString(),
                        MotDescCred = resultados[i]["motivo_situacao"].ToString(),
                        DataAbertura = resultados[i]["abertura"].ToString(),
                        Data_Cad = DateTime.Now,
                        Data_Alt = DateTime.Now, 
                        EmpresaId = usuario.EmpresaId

                    };
                    try
                    {
                        _clienteRepository.Adicionar(cliente);
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
                if (problemasAoSalvar > 0)
                {
                    if (cnpjErrado > 0)
                    {
                        TempData["Success"] = "Cnpjs Salvos: " + tcerto.ToString();
                        TempData["Error"] = "Cnpjs Inválidos ou com problemas: " + cnpjErrado.ToString() + " - Não salvo: " + problemasAoSalvar.ToString();
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
            this.listaClientes = await _clienteRepository.GetAll();
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

