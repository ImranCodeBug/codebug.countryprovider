using CodeBug.CountryProvider.Models;

namespace CodeBug.CountryProvider.Tests.Generator
{
    public static class CountryDataGenerator
    {
        public static Country GetCountryWithOutCode()
        {
            return new Country
            {
                Area = 45,
                Capital = "Bunios",
                Name = "Argentina",
                Population = 567342453,
                Region = "South America",
                SubRegion = "South America"
            };            
        }
    }
}
