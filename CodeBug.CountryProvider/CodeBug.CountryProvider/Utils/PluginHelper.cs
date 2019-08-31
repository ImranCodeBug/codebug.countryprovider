using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Interfaces;
using CodeBug.CountryProvider.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Utils
{
    public static class PluginHelper
    {
        public static ICountryGet PrepareCountryGet(Entity dataSourceEntity)
        {
            var url = GetAttributeValueFromDataSource(dataSourceEntity, RestCountrySchemaNames.ApiUrl);
            var headers = new Dictionary<string, string>
            {
                { GetAttributeValueFromDataSource(dataSourceEntity, RestCountrySchemaNames.HeaderKey1), GetAttributeValueFromDataSource(dataSourceEntity, RestCountrySchemaNames.HeaderValue1) },
                { GetAttributeValueFromDataSource(dataSourceEntity, RestCountrySchemaNames.HeaderKey2), GetAttributeValueFromDataSource(dataSourceEntity, RestCountrySchemaNames.HeaderValue2) }
            };
            var countryFormatter = new CountryFormatter();

            return new CountryGet(url, headers, countryFormatter);
        }

        private static string GetAttributeValueFromDataSource(Entity dataSourceEntity, string key)
        {
            if (!dataSourceEntity.Attributes.ContainsKey(key))
            {
                throw new ArgumentNullException(key);
            }

            return dataSourceEntity.GetAttributeValue<string>(key);
        }

        public static string GetContryCodeFromId(Guid countryId)
        {
            var countryDb = CountryCodeDb.CodeKeys;
            return countryDb.Where(t => t.Value == countryId).Select(k => k.Key).SingleOrDefault();
        }
    }
}
