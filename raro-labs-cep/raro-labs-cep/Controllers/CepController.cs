using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

using raro_labs_cep.Models;
using raro_labs_cep.Services;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace raro_labs_cep.Controllers
{
    /*
     Versionamento simples com a biblioteca microsoft.aspnet.mvc.versioning
     */
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CepController : ControllerBase
    {
        //declaração do objeto que receberá uma instância através da injeção de dependência.
        private readonly IConsultaCep _consultaCep;
        private readonly IConfiguration _config;

        public CepController(IConsultaCep consultaCep, IConfiguration config)
        {
            //referenciando o objeto instanciado no Startup
            _consultaCep = consultaCep;
            _config = config;
        }

        [ProducesResponseType((200), Type = typeof(Cep))]
        [ProducesResponseType((404), Type = typeof(Retorno))]
        [ProducesResponseType((400), Type = typeof(Retorno))]
        [HttpGet("consulta/{cep}")]
        public async Task<ActionResult> Get(string cep)
        {
            //Validação através de Regex verificando se há 8 caracteres e todos números.
            var validadorCep = new Regex(@"^\d{8}$");

            if (validadorCep.IsMatch(cep))
            {
                //Endereço base do serviço coletado do arquivo de configuração
                string baseAddress = _config["ViaCep:BaseUrl"];
                
                //consumindo da classe de serviços o cep com quantidade de caracteres correto e sequencial de números
                var resultado = await _consultaCep.ConsultaCepAsync(cep, baseAddress);

                //Caso tenha sucesso no retorno do CEP, um OK 200 com o Json contendo os dados do cep serão
                //retornados ao chamador, caso contrário, um NotFound 404 informando que não foi encontrado o repectivo cep
                return (resultado != null ? Ok(JsonConvert.SerializeObject(resultado)) : NotFound(new Retorno() { titulo = "Não encontrado", descricao = $"- O Cep {cep} não foi encontrado." }));
            }
            else
            {
                //Quando o cep informado possuir uma quantidade diferente da definida e houver caracteres divergentes ao requerido.
                return BadRequest(new Retorno() { titulo = "Inválido", descricao = $"- O Cep {cep} é inválido." });
            }
        }
    }
}
