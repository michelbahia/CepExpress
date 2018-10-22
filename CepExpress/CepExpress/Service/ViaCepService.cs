using ConsultarCep.Service.Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsultarCep.Service
{
    public class ViaCepService
    {
        //Singleton
        private static Lazy<ViaCepService> _Lazy = new Lazy<ViaCepService>(() => new ViaCepService());

        public static ViaCepService Current { get => _Lazy.Value; }

        private readonly HttpClient _HttpClient;

        private ViaCepService()
        {
            _HttpClient = new HttpClient();
        }

        //private static string EnderecoURL = "https://viacep.com.br/ws/{0}/json/";

        public async Task<Endereco> BuscarEnderecoViaCEPAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new InvalidOperationException("CEP não informado");

            try
            {
                using (var response = await _HttpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Algo deu errado ao cunsultar");

                    var result = await response.Content.ReadAsStringAsync();

                    if(string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Algo deu errado ao cunsultar");

                    return JsonConvert.DeserializeObject<Endereco>(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            //WebClient cliente = new WebClient();
            //string Conteudo = cliente.DownloadString(NovoEnderecoURL);

            //Endereco endereco = JsonConvert.DeserializeObject<Endereco>(Conteudo);

            //if (endereco.Cep == null)
            //    return null;

            //return endereco;
        }
    }
}
