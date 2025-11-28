using _360AI_BlazorApp.Components.Models;

namespace _360AI_BlazorApp.Components.Services
{
    public class SearchExecutorService
    {
        public async Task<OurModels[]> HandleSerch(string request)
        {


            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://screeny.ddns.net:8885/");
            var searchRequest = new
            {
                query = request,
                limit = 5
            };

            var response = await client.PostAsJsonAsync("http://screeny.ddns.net:8885/search", searchRequest);
            var result = await response.Content.ReadFromJsonAsync<OurModels[]>();
            return result;
        }

    }
}
