using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace TokioCity.Services
{
    public static class RequestHelper
    {
        public async static Task<T> GetData<T>(HttpClient client, string endpoint)
        {
            var request = await client.GetAsync(endpoint);
            try
            {
                if (request.IsSuccessStatusCode)
                {
                    string response = await request.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(response);
                    return result;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }
    }
}
