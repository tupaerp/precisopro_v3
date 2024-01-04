using Org.BouncyCastle.Bcpg.OpenPgp;

namespace PrecisoPRO.Services
{
    //Classe de serviço para consumo de APi Externa
    public class SefazToApiService
    {   
        //objeto http de requisições
        private readonly HttpClient _httpClient;

        //Construtor
        public SefazToApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            //Definição da URL base da API
            _httpClient.BaseAddress = new Uri("http://apps.sefaz.to.gov.br/cnd/servlet/hecwbcnd01");

        }

        //Serviço: FINALIDADE É FIXADO COMO CADASTRO (por enquanto)
        public async Task<HttpResponseMessage> ConsultarCnd(Dictionary<string, string> parametros)
        {
            //Criar form multpart
            var formContent = new MultipartFormDataContent();

            //Adicionar os parametros ao formulario
            foreach(var parametro in parametros)
            {
                formContent.Add(new StringContent(parametro.Value), parametro.Key);
            }

            //Fazer a solicitação post
            var resposta = await _httpClient.PostAsync("", formContent);

            //verificar se a solicitação foi bem-sucedida
            resposta.EnsureSuccessStatusCode();

            //retornar a resposta
            return resposta;
           
        }

    }
}
