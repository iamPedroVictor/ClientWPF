using FametroSistemas.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FametroSistemas.Service
{
    public class WebService
    {
        public string statusCode { get; private set; }
        public async Task<Usuario> Get(string uri){
            Usuario result = null;
            string data = "";
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Usuario>(@data);
                }
                this.statusCode = response.StatusCode.ToString();
            }
            return result;
        }

        public async Task<List<Usuario>> GetAll(string uri)
        {
            List<Usuario> result = null;
            string data = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Usuario>>(@data);
                }
                this.statusCode = response.StatusCode.ToString();
            }
            return result;
        }

        public async Task<string> Delete(string uri, List<HeaderParam> header)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                this.statusCode = response.StatusCode.ToString();
            }
            return result;
        }

        public async Task<Usuario> Atualizar(string uri, List<HeaderParam> header)
        {
            Usuario result = null;
            string data = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var headers = await Task.Run(() => JsonConvert.SerializeObject(
                                header.Where(h => (h.Key != null) && (h.Value != null))));

                HttpContent content = new StringContent(headers, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Usuario>(data);
                }
                this.statusCode = response.StatusCode.ToString();
            }
            return result;
        }

        public async Task<Usuario> Cadastrar(string uri, List<HeaderParam> header)
        {
            Usuario result = null;
            string data = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var headers = await Task.Run(() => JsonConvert.SerializeObject(header));

                HttpContent content = new StringContent(headers, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Usuario>(data);
                }
                this.statusCode = response.StatusCode.ToString();
            }
            return result;
        }


    }
}
