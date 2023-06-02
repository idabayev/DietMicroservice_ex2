using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ex1_ApiForMeals
{
    public class ProviderService : IProviderService
    {
        private readonly string _apiKey = "TPhH9zm9eYR3DRh/VDqSIA==Rvj0xo4mgKC6fwKK";
        private readonly string apiUrl = "https://api.api-ninjas.com/v1/nutrition";

        public async Task<List<Dish>> GetDishInfoFromExternalApiAsync(string name)
        {
            var httpClient = new HttpClient();
            var httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Get;
            var url = $"{apiUrl}?query={name}";
            httpRequest.RequestUri = new Uri(url);
            httpRequest.Headers.Add("X-Api-Key", _apiKey);
            var response = await httpClient.SendAsync(httpRequest);

            var res = await response.Content.ReadAsStringAsync();
            var dishes = JsonSerializer.Deserialize<List<Dish>>(res);
            return dishes;
        }
    }
}
