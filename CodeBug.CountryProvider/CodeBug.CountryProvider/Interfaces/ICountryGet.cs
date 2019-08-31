using CodeBug.CountryProvider.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Interfaces
{
    public interface ICountryGet
    {
        Task<IEnumerable<Entity>> RunSearch(string searchedBy, string searchedTerm);
        Task<IEnumerable<Entity>> AllAsync(string allText);
        Task<Entity> ByCodeAsync(string alpha3Code);
        Task<IEnumerable<Entity>> ByCapitalCityAsync(string capital);
        Task<IEnumerable<Entity>> ByRegionAsync(string region);
    }
}
