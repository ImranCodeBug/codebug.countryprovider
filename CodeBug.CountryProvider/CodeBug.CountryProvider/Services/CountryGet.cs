using CodeBug.CountryProvider.Interfaces;
using CodeBug.CountryProvider.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Services
{
    public class CountryGet : ICountryGet
    {
        private readonly string _url;
        private readonly Dictionary<string, string> _headers;
        private readonly IEntityFormatter<Country> _entityFormatter;
        private readonly Dictionary<string, Func<string, Task<IEnumerable<Entity>>>> _apiCaller 
            = new Dictionary<string, Func<string, Task<IEnumerable<Entity>>>>();
                        
        public CountryGet(string url, Dictionary<string, string> headers, IEntityFormatter<Country> entityFormatter)
        {
            _url = url;
            _headers = headers;
            _entityFormatter = entityFormatter;
            ConstructApiCaller();
        }

        public async Task<IEnumerable<Entity>> RunSearch(string searchedBy, string searchedTerm)
        {
            return await _apiCaller[searchedBy](searchedTerm);
        }

        public async Task<IEnumerable<Entity>> AllAsync(string allText)
        {
            var countries = await PerformGetAsync<Country[]>($"/{allText}");
            return countries.Select(t => _entityFormatter.ConvertToEntity(t));
        }

        public async Task<Entity> ByCodeAsync(string alpha3Code)
        {
            var country = await PerformGetAsync<Country>($"/alpha/{alpha3Code}");
            return _entityFormatter.ConvertToEntity(country);
        }

        public async Task<IEnumerable<Entity>> ByCapitalCityAsync(string capital)
        {
            var countries = await PerformGetAsync<Country[]>($"/capital/{capital}");
            return countries.Select(t => _entityFormatter.ConvertToEntity(t));
        }

        public async Task<IEnumerable<Entity>> ByRegionAsync(string region)
        {
            var countries = await PerformGetAsync<Country[]>($"/region/{region}");
            return countries.Select(t => _entityFormatter.ConvertToEntity(t));
        }

        private void ConstructApiCaller()
        {
            _apiCaller.Add("all", AllAsync);
            _apiCaller.Add("ic_capital", ByCapitalCityAsync);
            _apiCaller.Add("ic_region", ByRegionAsync);
        }

        private async Task<T> PerformGetAsync<T>(string urlString)
        {
            using (var httpClient = new HttpClient())
            {
                var endPoint = $"{_url}{urlString}";
                _headers.ToList().ForEach(t => 
                    httpClient.DefaultRequestHeaders.Add(t.Key.ToString(), t.Value.ToString()));
                var response = await httpClient.GetStreamAsync(endPoint);
                var deserializer = new DataContractJsonSerializer(typeof(T));

                return (T)deserializer.ReadObject(response);
            }            
        }
    }
}
