using _12_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _12_APIs
{
    public class SWAPIService
    {
        private HttpClient _client;

        public SWAPIService()
        {
            _client = new HttpClient();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://swapi.dev/api/people/{id}/");

            if (response.IsSuccessStatusCode)
            {
                Person person = await response.Content.ReadAsAsync<Person>();
                return person;
            }

            return new Person() { Name = "Nemo" };

        }

        public async Task<Planet> GetPlanetAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://swapi.dev/api/planets/{id}/");

            if (response.IsSuccessStatusCode)
            {
                Planet planet = await response.Content.ReadAsAsync<Planet>();
                return planet;
            }

            return new Planet() { Name = "Pouthena" };
        }
       
        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default;
        }

        public async Task<SearchResult<Person>> SearchPeopleAsync(string query)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://swapi.dev/api/people/?search={query}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<Person>>();
            }

            return new SearchResult<Person>();
        }

        public async Task<SearchResult<T>> SearchAsync<T>(string table, string query)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://swapi.dev/api/{table}/?search={query}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<T>>();
            }

            return new SearchResult<T>();
        }

        public async Task<SearchResult<StarShip>> SearchVehiclesAsync(string query)
        {
            return await SearchAsync<StarShip>("starship", query);
        }
        public async Task<SearchResult<Planet>> SearchPlanetAsync(string query)
        {
            return await SearchAsync<Planet>("planets", query);
        }
    }
}
