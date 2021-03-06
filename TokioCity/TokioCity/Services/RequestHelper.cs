﻿using Newtonsoft.Json;
using Newtonsoft;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace TokioCity.Services
{
    public static class RequestHelper
    {
        private static string version = "1.8";
        private static HttpClient clientStatic = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(10),
            BaseAddress = new Uri("https://www.tokyo-city.ru")
        };
        public async static Task<T> GetData<T>(HttpClient client, string endpoint)
        {
            var request = await client.GetAsync(endpoint + version);
            try
            {
                if (request.IsSuccessStatusCode)
                {
                    string response = await request.Content.ReadAsStringAsync();
                    if (typeof(T) == typeof(string))
                    {
                        return (T)Convert.ChangeType(response, typeof(T));
                    }
                    else
                    {
                        T result = JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        string test = JsonConvert.SerializeObject(result);
                        Console.WriteLine();
                        return result;
                    }
                    
                    
                }
                else
                {
                    return default(T);
                }
            }
            catch (TaskCanceledException e)
            {
                return default(T);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                return default(T);
            }
        }
    }
}
