using _360AI_BlazorApp.Components.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace _360AI_BlazorApp.Components.Services
{
    public class SearchExecutorService
    {
        public async Task<OurModels[]> HandleSerch(string request, HttpClient cl)
        {
            var searchRequest = new
            {
                query = request,
                limit = 5
            };

            var json = JsonSerializer.Serialize(new { query = request, limit = 5 });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cl.PostAsync("http://screeny.ddns.net:8885/search", content);
            var res = await response.Content.ReadFromJsonAsync<OurModels[]>();
            return res;
        }
    }
}
