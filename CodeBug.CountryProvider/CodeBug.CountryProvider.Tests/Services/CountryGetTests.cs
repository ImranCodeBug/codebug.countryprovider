using CodeBug.CountryProvider.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeBug.CountryProvider.Tests.Services
{
    public class CountryGetTests
    {
        private readonly string _url = $"https://restcountries-v1.p.rapidapi.com";
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>
        {
            { "X-RapidAPI-Host", "restcountries-v1.p.rapidapi.com" },
            { "X-RapidAPI-Key", "0c2bae8633msh53730119a91ba8ep111b44jsnc66e256ca290" }
        };
        private readonly CountryFormatter _countryFormatter = new CountryFormatter();

        private CountryGet _countryGet;
        
        public CountryGetTests()
        {
            _countryGet = new CountryGet(_url, _headers, _countryFormatter);
        }

        [Test]        
        public void AllAsync_NonEmptyResult()
        {
            var countries = _countryGet.AllAsync("all").Result;
            Assert.IsNotNull(countries);
            Assert.IsNotEmpty(countries);
        }

        [Test]
        public void ByCodeAsync_NonEmptyResult()
        {
            var country = _countryGet.ByCodeAsync("RUS").Result;
            Assert.IsNotNull(country);
        }

        [Test]
        public void ByRegionAsync_NonEmptyResult()
        {
            var countries = _countryGet.ByRegionAsync("africa").Result;
            Assert.IsNotNull(countries);
            Assert.IsNotEmpty(countries);
        }

        [Test]
        public void ByCapitalCityAsync_NonEmptyResult()
        {
            var countries = _countryGet.ByCapitalCityAsync("tallinn").Result;
            Assert.IsNotNull(countries);
            Assert.IsNotEmpty(countries);
        }
    }
}
