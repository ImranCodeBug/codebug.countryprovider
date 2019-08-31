using CodeBug.CountryProvider.Interfaces;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace CodeBug.CountryProvider.Services
{
    public class CountryQueryVisitor : IQueryExpressionVisitor
    {
        public string SearchingBy { get; private set; }
        public string SearchedTerm { get; private set; }
        private readonly IQueryValidator _queryValidator;
        
        public CountryQueryVisitor(IQueryValidator queryValidator)
        {
            _queryValidator = queryValidator;
        }

        public QueryExpression Visit(QueryExpression query)
        {
            SearchingBy = "all";
            SearchedTerm = "all";

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (_queryValidator.Validate())
            {
                SearchingBy = query.Criteria.Conditions.Single().AttributeName;
                SearchedTerm = query.Criteria.Conditions.Single().Values.Single().ToString();
            }
            return query;
        }
    }
}
