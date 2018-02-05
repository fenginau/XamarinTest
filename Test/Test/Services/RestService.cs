using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test.Models;
using Test.Utils;
using System.Text;

namespace Test.Services
{
    public class RestService
    {
        private readonly HttpClient _client;
        public RestService()
        {
            _client = InitializeClient();
        }

        public HttpClient InitializeClient()
        {
            var authBytes = System.Text.Encoding.UTF8.GetBytes(GlobalVariable.HardwareId + ":0101");
            var client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000,
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes)),
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };
            return client;
        }

        private async Task<string> GetAsync(string apiPath)
        {
            try
            {
                var uri = new Uri(GlobalVariable.ApiBaseUrl + apiPath);
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    return content;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("some thing is wrong" + e.Message);
            }
            return null;
        }

        public async Task<HttpResultModel> GetChats(int convId)
        {
            var content = await GetAsync($"messageapi/getchat/{GlobalVariable.HardwareId}/{convId}?{DateTime.Now.Ticks}");
            if (content != null)
            {
                var messages = JsonConvert.DeserializeObject<ConversationModel>(content);
                return new HttpResultModel { IsSuccess = true, Content = messages };
            }
            return new HttpResultModel { IsSuccess = false, Content = null };
        }

        public async Task<HttpResultModel> PostAsync()
        {
            try
            {
                StringContent sc = new StringContent("{ InvoiceNumber: '123' }", Encoding.UTF8, "application/json");
                var uri = new Uri(@"http://192.168.10.70/word/api/mergeword");
                var response = await _client.PostAsync(uri, sc);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new HttpResultModel { IsSuccess = false, Content = null };
        }
    }
}
