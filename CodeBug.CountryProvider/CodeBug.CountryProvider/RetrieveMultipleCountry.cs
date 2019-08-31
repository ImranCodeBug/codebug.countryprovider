using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Services;
using CodeBug.CountryProvider.Utils;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace CodeBug.CountryProvider
{
    public class RetrieveMultipleCountry : IPlugin
    {
        private readonly string _businessEntityColletion = "BusinessEntityCollection";
        private readonly string _queryParamName = "Query";
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var retriverService = (IEntityDataSourceRetrieverService)serviceProvider.GetService(typeof(IEntityDataSourceRetrieverService));
            var query = (QueryExpression)context.InputParameters[_queryParamName];

            var sourceEntity = retriverService.RetrieveEntityDataSource();

            var queryValidator = new QueryValidator(tracingService, query);
            var queryVisitor = new CountryQueryVisitor(queryValidator);

            query.Accept(queryVisitor);

            var countryGet = PluginHelper.PrepareCountryGet(sourceEntity);            
            
            var countryEntityCollection = countryGet.RunSearch(queryVisitor.SearchingBy, queryVisitor.SearchedTerm)
                .Result.ToList();

            var entityColletion = new EntityCollection
            {
                EntityName = CountrySchemaNames.EntitySchemaName
            };

            entityColletion.Entities.AddRange(countryEntityCollection.ToList());

            context.OutputParameters[_businessEntityColletion] = entityColletion;
        }       
    }
}
