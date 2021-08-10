using System.Threading.Tasks;
using raro_labs_cep.Models;

namespace raro_labs_cep.Services
{
    public interface IConsultaCep
    {
        Task<Cep> ConsultaCepAsync(string cep, string baseAddress); 
    } 
}