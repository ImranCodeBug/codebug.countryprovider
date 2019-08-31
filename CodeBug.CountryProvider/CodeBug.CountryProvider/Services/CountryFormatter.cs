using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Interfaces;
using CodeBug.CountryProvider.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Services
{
    public class CountryFormatter : IEntityFormatter<Country>
    {
        public Entity ConvertToEntity(Country resultItem)
        {
            if (resultItem == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(resultItem.Code))
            {
                throw new InvalidPluginExecutionException($"Code missing in the country record");
            }

            var entity = new Entity(CountrySchemaNames.EntitySchemaName);
            entity[CountrySchemaNames.CountryId] = CountryCodeDb.CodeKeys[resultItem.Code];

            entity[CountrySchemaNames.Area] = resultItem.Area;
            entity[CountrySchemaNames.Capital] = resultItem.Capital;
            entity[CountrySchemaNames.Code] = resultItem.Code;
            entity[CountrySchemaNames.Name] = resultItem.Name;
            entity[CountrySchemaNames.Population] = resultItem.Population;
            entity[CountrySchemaNames.Region] = resultItem.Region;
            entity[CountrySchemaNames.SubRegion] = resultItem.SubRegion;
            return entity;
        }
    }
}
