using CryptoStats.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoStats.Services
{
    public class CoincapApiService
    {
        public HttpClient httpClient { get; private set; }
        public HttpRequestMessage? httpRequestMessage { get; private set; }
        public HttpResponseMessage? httpResponseMessage { get; private set; }

        private static string BearerToken => "06b85496-705b-4541-b4dd-3d2746eb2e82";

        public CoincapApiService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/");
        }

        public async Task<List<Cryptocurrency>?> RequestCryptocurrenciesAsync()
        {
            httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "assets/");
            httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var jsonResultAsTaskStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonArray = JsonConvert.DeserializeObject<ResponseCoinCapData>(jsonResultAsTaskStringAsync);
            return jsonArray?.Data;
        }
    }
}
