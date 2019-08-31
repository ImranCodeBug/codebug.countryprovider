using CodeBug.CountryProvider.Constants;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Tests.Generator
{
    public static class QueryExpressionGenerator
    {
        private static readonly string entityName = "customeentity";
        public static QueryExpression GenerateQueryExpressionWithMultipleCondition()
        {
            var query = new QueryExpression(entityName);
            query.Criteria.AddCondition("WtrongField", ConditionOperator.Equal, "Hello");
            query.Criteria.AddCondition(CountrySchemaNames.Area, ConditionOperator.EndsWith, "Hello");
            return query;
        }

        public static QueryExpression GenerateQueryExpressionWithWrongField()
        {
            var query = new QueryExpression(entityName);
            query.Criteria.AddCondition("WtrongField", ConditionOperator.Equal, "Hello");
            return query;
        }

        public static QueryExpression GenerateQueryExpressionWithWrongOperator()
        {
            var query = new QueryExpression(entityName);
            query.Criteria.AddCondition(CountrySchemaNames.Area, ConditionOperator.EndsWith, "Hello");
            return query;
        }

        public static QueryExpression GenerateQueryExpressionWithCorrectCondtion()
        {
            var query = new QueryExpression(entityName);
            query.Criteria.AddCondition(CountrySchemaNames.Region, ConditionOperator.Equal, "Hello");
            return query;
        }
    }
}
