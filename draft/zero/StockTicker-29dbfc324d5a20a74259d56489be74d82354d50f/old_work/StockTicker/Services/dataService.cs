using StockTicker.Models;
using System.Collections;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StockTicker.Services
{
    public class dataService :IGet
    {
        private readonly HttpClient _httpClient;

        public dataService( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Stock>?> GetData(string ticker, string accTok)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bear", accTok);

            var response = await _httpClient.GetAsync($"/v2/aggs/ticker/{ticker}/range/1/month/2019-07-01/2021-06-01?adjusted=true&sort=asc");

            response.EnsureSuccessStatusCode();

            using var restream = await response.Content.ReadAsStreamAsync();

            var Res_obj = await JsonSerializer.DeserializeAsync<Polygon>(restream);

            int j = 1;
            return Res_obj?.results.Select(i => new Stock
            {
                
                tick = Res_obj?.ticker,
                date = j ,
                price = (decimal)i.c,

            });
        }    
    }
}
