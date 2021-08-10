using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using raro_labs_cep.Models;

namespace raro_labs_cep.Services
{
    public class ConsultaCep : IConsultaCep
    {
        public async Task<Cep> ConsultaCepAsync(string cep, string baseAddress)
        {
            //declarando o endpoint
            string endPoint = $"ws/{cep}/json/";

            //Instanciando um client que irá consumir o serviço que retorna os dados do CEP informado
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(baseAddress);

            //Determina o tipo de retorno do serviço (Json)
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            
            //Faz uma chamada do tipo GET de forma assincrona para evitar que a execução passe direto sem a resposta esperada.
            HttpResponseMessage response = await client.GetAsync(endPoint);
            
            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadAsAsync<Cep>();
                //O Retorno pode ter tido sucesso ao ser chamado, porém, sem nenhum dado recuperado.
                return (retorno.cep != null ? retorno : null);
            }

            //Caso não tenha sucesso! retorno nulo!
            return null;
        }
    }
}