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
            //return [new() { Content = "Это типо реквест приходит!", Id = "1", MediaType="text", Relevance = 0.1232411, Source="C:/Test.css" }, new() { Content = "здесь было круто!", Id = "asndoansd", MediaType = "text", Relevance = 0.1232411, Source = "C:/Sochi2004.txt" }, new() { Content = "std::out >> xtep.txt", Id = "iLoveEverybody", MediaType = "image", Relevance = 0.1232411, Source = "C:\\Users\\Denoos\\source\\repos\\360AI\\360AI_BlazorApp\\wwwroot\\images\\image_preza.png" }];
           
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
