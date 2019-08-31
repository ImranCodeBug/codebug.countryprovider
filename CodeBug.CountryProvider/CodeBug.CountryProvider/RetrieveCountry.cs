using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Utils;
using Microsoft.Xrm.Sdk;
using System;

namespace CodeBug.CountryProvider
{
    public class RetrieveCountry : IPlugin
    {
        private readonly string _businessEntity = "BusinessEntity";
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var retriverService = (IEntityDataSourceRetrieverService)serviceProvider.GetService(typeof(IEntityDataSourceRetrieverService));
            var countryId = context.PrimaryEntityId;

            var sourceEntity = retriverService.RetrieveEntityDataSource();

            var countryGet = PluginHelper.PrepareCountryGet(sourceEntity);
            var countryCode = PluginHelper.GetContryCodeFromId(countryId);

            var countryEntity = countryGet.ByCodeAsync(countryCode).Result;            
            context.OutputParameters[_businessEntity] = countryEntity;
        }
    }
}
